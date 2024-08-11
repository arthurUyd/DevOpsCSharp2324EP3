using Microsoft.EntityFrameworkCore;
using SVK.Persistence;
using SVK.Domain.Gebruikers;
using SVK.Shared.Gebruikers;
namespace SVK.Services.Gebruikers;

public class GebruikerService : IGebruikerService
{
    private readonly ApplicationDBContext dbContext;

    public GebruikerService(ApplicationDBContext dbContext)
    {
        this.dbContext = dbContext;
    }
/*
    public async Task<int> CreateAsync(GebruikerDto.Mutate model)
    {
        Gebruiker g = new(model.Naam!); 
        dbContext.Gebruikers.Add(g);
        await dbContext.SaveChangesAsync();
        return g.Id;    
    }

    public async Task<GebruikerDto.Detail> GetDetailAsync(int id)
    {
        GebruikerDto.Detail? gebruiker = await dbContext.Gebruikers.Select(x => new GebruikerDto.Detail
        {
            Id = x.Id,
            Naam = x.Naam,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
        }).SingleOrDefaultAsync(x => x.Id == id);

        if(gebruiker is null  ) 
            throw new EntityNotFoundException(nameof(gebruiker), id);
        return gebruiker;
    }*/

    public async Task<GebruikerResult.Index> GetIndexAsync(GebruikerRequest.Index request)
    {
       var query = dbContext.Gebruikers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Naam.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new GebruikerDto.Index
           {
               Id = x.Id,
               Naam = x.Naam,
           }).ToListAsync();

        var result = new GebruikerResult.Index
        {
            Gebruikers = items,
            TotalAmount = totalAmount
        };

        return result;
    }
}
