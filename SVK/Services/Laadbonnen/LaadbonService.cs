using Domain.Laadbonnen;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Laadbonnen;
using Shared.Producten;

namespace Services.Laadbonnen;
internal class LaadbonService : ILaadbonService
{
    private readonly ApplicationDBContext dbContext;
    public LaadbonService(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(int transportopdrachtid, LaadbonDto.Mutate model)
    {
        if (await dbContext.Laadbonnen.AnyAsync(x => x.Nummer == model.Nummer))
             throw new EntityAlreadyExistsException(nameof(Laadbon), nameof(Laadbon.Nummer), model.Nummer.ToString());

        Laadbon l = new(model.Nummer!.Value);
        dbContext.Laadbonnen.Add(l);
        await dbContext.SaveChangesAsync();

        return l.Id;
    }

    public async Task<LaadbonDto.Detail> GetDetailAsync(int laadbonnnummer)
    {
        LaadbonDto.Detail? detail = await dbContext.Laadbonnen.Select(x => new LaadbonDto.Detail
        {
            Id = x.Id,
            Nummer = x.Nummer
        }).SingleOrDefaultAsync(x => x.Nummer == laadbonnnummer);

        if(detail is null)
            throw new EntityNotFoundException(nameof(Laadbon), laadbonnnummer);
        return detail;
    }

    public async Task<LaadbonResult.Index> GetIndexAsync(LaadbonRequest.Index request)
    {
        var query = dbContext.Laadbonnen.AsQueryable();

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new LaadbonDto.Index
           {
               Id = x.Id,
               Nummer = x.Nummer,
           }).ToListAsync();

        var result = new LaadbonResult.Index
        {
            Laadbonnen = items,
            TotalAmount = totalAmount
        };
        return result;
    }
}

