using System;
using System.Threading.Tasks;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Context.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IUnitOfWork, IDisposable
  {
    private readonly CampgroundContext _context;
    private bool _disposedValue;

    public ICampgroundRepo Campground { get; }
    public IRepository<CampsiteModel> Campsite { get; set; }
    public IRepository<ReviewModel> Review { get; set; }
    public IRepository<ImageModel> Image { get; set; }

    public UnitOfWork(CampgroundContext context)
    {
      _context = context;

      Campground = new CampgroundRepo(context);
      Campsite = new Repository<CampsiteModel>(context);
      Review = new Repository<ReviewModel>(context);
      Image = new Repository<ImageModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _context.Dispose();
        }
        _disposedValue = true;
      }
    }
    public void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
