using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Interfaces;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    public readonly DbSet<TEntity> Db;

    public Repository(LodgingContext context)
    {
      Db = context.Set<TEntity>();
    }

    public virtual async Task DeleteAsync(int id) => Db.Remove(await SelectAsync(id));

    public virtual async Task InsertAsync(TEntity entry) => await Db.AddAsync(entry).ConfigureAwait(true);

    public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await Db.ToListAsync();

    public virtual async Task<TEntity> SelectAsync(int id)
    {
      var result = await Db.FindAsync(id).ConfigureAwait(true);
      if (result == null)
      {
        throw new KeyNotFoundException("The given id does not exist in the database.");
      }
      else
      {
        return result;
      }
    }

    public virtual void Update(TEntity entry) => Db.Update(entry);
  }
}
