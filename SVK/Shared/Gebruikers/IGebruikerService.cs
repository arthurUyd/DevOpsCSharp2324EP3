using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Gebruikers;

public interface IGebruikerService
{
    Task<GebruikerResult.Index> GetIndexAsync(GebruikerRequest.Index request);
    Task<GebruikerDto.Detail> GetDetailAsync(int id);
    Task<int> CreateAsync(GebruikerDto.Mutate model);
}
