using System.Threading.Tasks;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Context.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly LodgingContext _context;
    private bool _disposedValue;

    public ILodgingRepo Lodging { get; }
    public IRepository<RentalModel> Rental { get; set; }
    public IRepository<ReviewModel> Review { get; set; }
    public IRepository<ImageModel> Image { get; set; }

    public UnitOfWork(LodgingContext context)
    {
      _context = context;

      Lodging = new LodgingRepo(context);
      Rental = new Repository<RentalModel>(context);
      Review = new Repository<ReviewModel>(context);
      Image = new Repository<ImageModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
