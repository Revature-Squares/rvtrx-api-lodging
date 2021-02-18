using System.Collections.Generic;
using System.Threading.Tasks;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Interfaces
{
  public interface ICampgroundRepo : IRepository<CampgroundModel>
  {
    Task<IEnumerable<CampgroundModel>> CampgroundByLocationAndOccupancy(int occupancy, params string[] location);
  }
}
