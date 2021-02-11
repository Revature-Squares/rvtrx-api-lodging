using System.Threading.Tasks;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Interfaces
{
  public interface IUnitOfWork
  {
    ILodgingRepo Lodging { get; }
    IRepository<RentalModel> Rental { get; set; }
    IRepository<ReviewModel> Review { get; set; }
    IRepository<ImageModel> Image { get; set; }

    Task<int> CommitAsync();
  }
}
