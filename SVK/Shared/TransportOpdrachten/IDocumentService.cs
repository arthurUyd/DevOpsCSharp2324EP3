using Shared.Producten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TransportOpdrachten;

public interface IDocumentService
{
    Task<DocumentResult.Index> GetIndexAsync(DocumentRequest.Index request);
    Task<DocumentDto.Detail> GetDetailAsync(int docId);
    Task<int> CreateAsync(DocumentDto.Mutate model);
}
