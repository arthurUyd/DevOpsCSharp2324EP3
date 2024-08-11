using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Shared.Laadbonnen;

public interface ILaadbonService
{
    Task<LaadbonResult.Create> CreateAsync(int id,LaadbonDto.Mutate model);
  
}
