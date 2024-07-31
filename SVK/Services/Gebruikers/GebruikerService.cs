using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Gebruikers;
namespace Services.Gebruikers;

public class GebruikerService : IGebruikerService
{
    private readonly ApplicationDBContext _context;
    public Task<GebruikerResult.Create> CreateAsync(GebruikerDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task<GebruikerDto.Index> GetGebruikerByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<GebruikerDto.Index[]> GetIndexAsync()
    {
        throw new NotImplementedException();
    }
}
