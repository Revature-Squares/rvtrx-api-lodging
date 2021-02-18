using System.Collections.Generic;
using System.Linq;
using RVTR.Lodging.Context;
using RVTR.Lodging.Context.Repositories;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class CampgroundRepoTest : DataTest
  {
    public static readonly IEnumerable<object[]> Records = new List<object[]>
    {
      new object[]
      {
        new CampgroundModel
        {
          Id = 5,
          Name = "Campground",
          Address = new AddressModel
          {
            Id = 100,
            City = "Austin",
            StateProvince = "TX",
            Country = "USA",
            PostalCode = "11111",
            Street = "Street",
            Longitude = "1.00N",
            Latitude = "1.00W"
          },
          Campsites = new List<CampsiteModel>
          {
            new CampsiteModel()
            {
              Id = 100, LotNumber = "1", Status = "Available", SiteName = "Unit1", Size = "5x5", Capacity = 4
            },
            new CampsiteModel()
            {
              Id = 101, LotNumber = "2", Status = "Booked", SiteName = "Unit2", Size = "5x5", Capacity = 4
            }
          }
        }
      }
    };

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_CampgroundRepo_SelectAsync(CampgroundModel campground)
    {
      using (var ctx = new CampgroundContext(Options))
      {
        await ctx.Campgrounds.AddAsync(campground);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campgrounds = new CampgroundRepo(ctx);

        var resultEqual = await campgrounds.SelectAsync(5);

        Assert.Equal(campground.Id, resultEqual.Id);
        Assert.Equal(campground.Bathrooms, resultEqual.Bathrooms);
        Assert.Equal(campground.Address.Latitude, resultEqual.Address.Latitude);
        Assert.Equal(campground.Address.Longitude, resultEqual.Address.Longitude);
        Assert.Equal(campground.Name, resultEqual.Name);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await campgrounds.SelectAsync(-1));
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_CampgroundRepo_CampgroundByLocationAndOccupancy(CampgroundModel campground)
    {
      using (var ctx = new CampgroundContext(Options))
      {
        await ctx.Campgrounds.AddAsync(campground);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new CampgroundContext(Options))
      {
        var campgrounds = new CampgroundRepo(ctx);

        var actual = await campgrounds.CampgroundByLocationAndOccupancy(2, "Austin");

        Assert.NotEmpty(actual);
        Assert.True(actual.Count() == 1);
      }
    }
  }
}
