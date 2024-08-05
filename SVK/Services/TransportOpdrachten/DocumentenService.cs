using Domain.Files;
using Microsoft.EntityFrameworkCore;
using NHibernate.Linq.Expressions;
using Persistence;
using Shared.Laadbonnen;
using Shared.TransportOpdrachten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TransportOpdrachten;

public class DocumentenService : IDocumentService
{
    private readonly ApplicationDBContext dBContext;

    public DocumentenService(ApplicationDBContext dBContext)
    {
        this.dBContext = dBContext;
    }
    public async Task<int> CreateAsync(DocumentDto.Mutate model)
    {
        var document = new Document
        (
           Path.GetFileName(model.File!.FileName),
           model.File.ContentType,
           model.File.Length
        );
        using (var memoryStream = new MemoryStream())
        {
            await model.File.CopyToAsync(memoryStream);
            document.Content = memoryStream.ToArray();
        }


        dBContext.Documenten.Add(document);
        await dBContext.SaveChangesAsync();
        return document.Id;
    }

    public async Task<DocumentDto.Detail> GetDetailAsync(int docId)
    {
       DocumentDto.Detail? detail = await dBContext.Documenten.Select(x => new DocumentDto.Detail
       {
           Id = x.Id,
           Name = x.Name,
           ContentType = x.ContentType,
           Size = x.Size,
           Content = x.Content,
       }).SingleOrDefaultAsync(x => x.Id == docId);

        if(detail is null)  
            throw new EntityNotFoundException(nameof(Document), docId);
        
        return detail;
    }

    public async Task<DocumentResult.Index> GetIndexAsync(DocumentRequest.Index request)
    {
        var query = dBContext.Documenten.AsQueryable();

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new DocumentDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               ContentType = x.ContentType,
               Size = x.Size,
               Content = x.Content,
           }).ToListAsync();

        var result = new DocumentResult.Index
        {
            Documenten = items,
            TotalAmount = totalAmount
        };
        return result;
    }
}
