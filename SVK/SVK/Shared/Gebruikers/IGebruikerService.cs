using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Gebruikers;

public interface IGebruikerService
{
    Task<GebruikerResult.Index> GetIndexAsync(GebruikerRequest.Index request);
  
}
