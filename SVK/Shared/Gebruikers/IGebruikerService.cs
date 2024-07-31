using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Gebruikers;

public interface IGebruikerService
{
    Task<GebruikerDto.Index[]> GetIndexAsync();
    Task<GebruikerResult.Create> CreateAsync(GebruikerDto.Mutate model);
    Task<GebruikerDto.Index> GetGebruikerByIdAsync(Guid id);
}
