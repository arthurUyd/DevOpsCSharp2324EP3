using SVK.Domain.Gebruikers;
using SVK.Domain.TransportOpdrachten;
using Microsoft.EntityFrameworkCore;
using SVK.Persistence;
using SVK.Services.Files;
using SVK.Domain.Files;
using SVK.Shared.Gebruikers;
using SVK.Shared.TransportOpdrachten;
using SVK.Shared.Laadbonnen;
using SVK.Shared.Producten;


namespace SVK.Services.TransportOpdrachten;

public class TransportOpdrachtenService : ITransportOpdrachtService
{
    private readonly ApplicationDBContext dBContext;
    private readonly IStorageService storageService;

    public TransportOpdrachtenService(ApplicationDBContext dBContext, IStorageService storageService)
    {
        this.dBContext = dBContext;
        this.storageService = storageService;
    }

    public async Task<TransportOpdrachtResult.Create> CreateAsync(TransportOpdrachtDto.Mutate model)
    {
        if (await dBContext.TransportOpdrachten.AnyAsync(x => x.Routenummer == model.Routenummer))
            throw new EntityAlreadyExistsException(nameof(TransportOpdracht), nameof(TransportOpdracht.Routenummer), model.Routenummer.ToString());
        
        Gebruiker g = await dBContext.Gebruikers
         .Where(x => x.Naam == model.Lader)
         .FirstOrDefaultAsync();

        if (g == null)
        {
            g = new Gebruiker(model.Lader);
            dBContext.Gebruikers.Add(g);  
            await dBContext.SaveChangesAsync(); 
        }

        Image image = new Image(storageService.BasePath, model.ImageContentType!);


        TransportOpdracht t = new(model.Datum!.Value, model.Routenummer!.Value, g, image.FileUri.ToString(), model.Nummerplaat!);

        dBContext.TransportOpdrachten.Add(t);
        await dBContext.SaveChangesAsync();

        Uri uploadSas = storageService.GenerateImageUploadSas(image);

        TransportOpdrachtResult.Create result = new()
        {
            TransportOpdrachtId = t.Id,
            UploadUri = uploadSas.ToString(),
        };

        return result;
    }

    public async Task<TransportOpdrachtDto.Detail> GetDetailAsync(int id)
    {
        TransportOpdrachtDto.Detail? t = await dBContext.TransportOpdrachten.Select(x => new TransportOpdrachtDto.Detail
        {
            Id = x.Id,
            Datum = x.Datum,
            Routenummer = x.Routenummer,
            Laadbonnen = x.Laadbonnen != null
            ? x.Laadbonnen.Select(e => new LaadbonDto.Detail
            {
                Nummer = e.Nummer,
                Url = e.Bestandurl,
                Address = new AddressDto
                {
                    Addressline1 = e.Address.Addressline1,
                    Addressline2 = e.Address.Addressline2,
                    PostalCode = e.Address.PostalCode,
                    City = e.Address.City,
                    Country = e.Address.Country,
                },
                Transporteur = e.Transporteur,
                Producten = e.Producten.Select(a => new ProductDto.Index
                {
                    ProductNaam = a.ProductNaam,
                })
            }).ToList()
            : new List<LaadbonDto.Detail>(),
            Lader = new GebruikerDto.Index
            {
                Naam = x.Lader.Naam
            },
            Fotourl = x.FotoUrl,
            
            Nummerplaat = x.Nummerplaat,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).SingleOrDefaultAsync(x => x.Id == id);

        if (t is null)
            throw new EntityNotFoundException(nameof(TransportOpdracht), id);

        return t;
    }

    public async Task<TransportOpdrachtResult.Index> GetIndexAsync(TransportOpdrachtRequest.Index request)
    {
        var query = dBContext.TransportOpdrachten.AsQueryable();

        

        if(!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Lader.Naam.ToLower().Contains(request.Searchterm.ToLower())
            || x.Routenummer.ToString().Contains(request.Searchterm));
        }
        

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new TransportOpdrachtDto.Index
           {
               Id = x.Id,
               Datum = x.Datum,
               Routenummer = x.Routenummer,
               Laadbonnen = x.Laadbonnen.Select(e => new LaadbonDto.Index
               {
                   Nummer = e.Nummer,
                   Url = e.Bestandurl
               }),
               Lader = new GebruikerDto.Index
               {
                   Naam = x.Lader.Naam
               },
               Fotourl = x.FotoUrl,
               Nummerplaat = x.Nummerplaat,
           }).ToListAsync();

        var result = new TransportOpdrachtResult.Index
        {
            TransportOpdrachten = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    public async Task EditAsync(int transportOpdrachtId, TransportOpdrachtDto.Mutate model)
    {
        TransportOpdracht? opdracht = await dBContext.TransportOpdrachten.SingleOrDefaultAsync(x => x.Id == transportOpdrachtId);

        if (opdracht is null)
            throw new EntityNotFoundException(nameof(TransportOpdracht), transportOpdrachtId);

        Gebruiker g = await dBContext.Gebruikers
       .Where(x => x.Naam == model.Lader)
       .FirstOrDefaultAsync();

        if (g == null)
        {
            g = new Gebruiker(model.Lader);
            dBContext.Gebruikers.Add(g);
            await dBContext.SaveChangesAsync();
        }
       
        opdracht.Datum = model.Datum!.Value;
        opdracht.Routenummer = model.Routenummer!.Value;
        opdracht.Lader = g;
        opdracht.Nummerplaat = model.Nummerplaat!;

        await dBContext.SaveChangesAsync();


    }
}
