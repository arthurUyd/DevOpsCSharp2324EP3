using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TransportOpdrachten;

public interface ITransportOpdrachtService
{
    Task<TransportOpdrachtDto.Detail> GetDetailAsync(int id);
    Task<TransportOpdrachtResult.Index> GetIndexAsync(TransportOpdrachtRequest.Index request);
    Task<TransportOpdrachtResult.Create> CreateAsync(TransportOpdrachtDto.Mutate model);
    Task EditAsync(int transportOpdrachtId, TransportOpdrachtDto.Mutate model);

}
