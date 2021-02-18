using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Context.Repositories
{
  public class CampgroundRepo : Repository<CampgroundModel>, ICampgroundRepo
  {
    public CampgroundRepo(CampgroundContext context) : base(context)
    { }

    /// <summary>
    /// This Method will select all campgrounds, including their Campsites, Location, Reviews, and Locations Address
    /// </summary>
    public override async Task<IEnumerable<CampgroundModel>> SelectAsync() => await Db
      .Include(r => r.Campsites)
      // .ThenInclude(ru => ru.Unit)
      .Include(a => a.Address)
      .Include(r => r.Reviews)
      .Include(i => i.Images)
      .ToListAsync();

    /// <summary>
    /// This method will get a Campground with the given Id and will include its Location, Reviews, and the locations address
    /// </summary>
    public override async Task<CampgroundModel> SelectAsync(int id)
    {
      var campground = await Db
      .Include(r => r.Campsites)
      // .ThenInclude(ru => ru.Unit)
      .Include(a => a.Address)
      .Include(r => r.Reviews)
      .Include(i => i.Images)
      .FirstOrDefaultAsync(x => x.Id == id);
      if (campground == null)
      {
        throw new KeyNotFoundException("The given id does not exist in the database.");
      }
      else
      {
        return campground;
      }
    }

    /// <summary>
    /// This method will return all the campgrounds in the given location whose campsite status is "available" and where occupancy is not less than the
    /// desired occupancy. It will include the Campsites, Location, and Address tables in its non-case-sensitive filter action. Optional fields
    /// for City, State/Province, or Country that are either null or empty are ignored. These parameters must be entered as arguments in that order.
    /// </summary>
    public async Task<IEnumerable<CampgroundModel>> CampgroundByLocationAndOccupancy(int occupancy, params string[] location)
    {
      var numParams = location.Length;

      Expression<Func<CampgroundModel, bool>> matchesAll = c =>
        (numParams < 1 || string.IsNullOrEmpty(location[0]) || c.Address.City.ToLower() == location[0].ToLower()) &&
        (numParams < 2 || string.IsNullOrEmpty(location[1]) || c.Address.StateProvince.ToLower() == location[1].ToLower()) &&
        (numParams < 3 || string.IsNullOrEmpty(location[2]) || c.Address.Country.ToLower() == location[2].ToLower());

      var campgroundsByLocation = await Db
        .Include(i => i.Images)
        .Include(r => r.Campsites)
        // .ThenInclude(ru => ru.Unit)
        .Include(la => la.Address)
        .Where(matchesAll)
        .Where(x => x.Campsites.Any(y => y.Status == "Available" && y.Capacity >= occupancy))
        .ToListAsync();

      return campgroundsByLocation;
    }
  }
}
