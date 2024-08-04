using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Laadbonnen;

public interface ILaadbonService
{
    Task<int> CreateAsync(int id,LaadbonDto.Mutate model);
    Task<LaadbonDto.Detail> GetDetailAsync(int id);
    Task<LaadbonResult.Index> GetIndexAsync(LaadbonRequest.Index request);

}
