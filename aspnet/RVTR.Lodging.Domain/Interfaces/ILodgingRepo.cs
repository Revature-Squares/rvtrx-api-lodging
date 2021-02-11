using System.Collections.Generic;
using System.Threading.Tasks;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Interfaces
{
  public interface ILodgingRepo : IRepository<LodgingModel>
  {
    Task<IEnumerable<LodgingModel>> LodgingByLocationAndOccupancy(int occupancy, params string[] location);
  }
}
