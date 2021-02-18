using System.Threading.Tasks;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Interfaces
{
  public interface IUnitOfWork
  {
    ICampgroundRepo Campground { get; }
    IRepository<CampsiteModel> Campsite { get; set; }
    IRepository<ReviewModel> Review { get; set; }
    IRepository<ImageModel> Image { get; set; }

    Task<int> CommitAsync();
  }
}
