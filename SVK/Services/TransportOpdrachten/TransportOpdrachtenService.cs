using Domain.Gebruikers;
using Domain.TransportOpdracht;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Files;
using Domain.Files;
using Shared.Gebruikers;
using Shared.Producten;
using Shared.TransportOpdrachten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Files.File;

namespace Services.TransportOpdrachten;

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
        Gebruiker g = new(model.Lader!);
        File image = new File(storageService.BasePath, model.ImageContentType!);


        TransportOpdracht t = new(model.Datum!.Value, model.Routenummer!.Value, g,image.FileUri.ToString(), model.Transporteur!, model.Nummerplaat!);

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
            Id  = x.Id, 
            Datum = x.Datum,
            Routenummer = x.Routenummer,
            Laadbonnummers = x.Laadbonnummers,  
            Lader = new GebruikerDto.Index
            {
                Naam = x.Lader.Naam
            },
            Fotourl = x.FotoUrl,
            BestandenUrls = x.BestandenUrls,
            Transporteur = x.Transporteur,
            Nummerplaat = x.Nummerplaat,
            Producten = x.Producten.Select(x => x.ProductNaam), 
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
               Laadbonnummers = x.Laadbonnummers,
               Lader = new GebruikerDto.Index
               {
                   Naam = x.Lader.Naam
               },
               Fotourl = x.FotoUrl,
               BestandenUrls = x.BestandenUrls,
               Transporteur = x.Transporteur,
               Nummerplaat = x.Nummerplaat,
               Producten = x.Producten.Select(x => x.ProductNaam),
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
       
        Gebruiker lader = new(model.Lader!);
        opdracht.Datum = model.Datum!.Value;
        opdracht.Routenummer = model.Routenummer!.Value;
        opdracht.Lader = lader;
        opdracht.Transporteur = model.Transporteur!;
        opdracht.Nummerplaat = model.Nummerplaat!;

        await dBContext.SaveChangesAsync();
        

    }
}
