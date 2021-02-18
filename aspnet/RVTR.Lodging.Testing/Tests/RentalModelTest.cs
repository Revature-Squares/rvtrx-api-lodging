using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class CampsiteModelTest
  {
    public static readonly IEnumerable<object[]> Campsites = new List<object[]>
    {
      new object[]
      {
        new CampsiteModel
        {
          Id = 0,
          Status = "Available",
          Price = 0.0,
          DiscountedPrice = 0.0,
          CampgroundModelId = 0,
          LotNumber = "1",
          CapacityPeople = 1,
          CapacityVehicles = 0,
          SiteName = "TestCampsite",
          Campground = new CampgroundModel(),
          Type = CampsiteModel.CampsiteType.Backpacking
        }
      }
    };

    [Theory]
    [MemberData(nameof(Campsites))]
    public void Test_Create_CampsiteModel(CampsiteModel campsite)
    {
      var validationContext = new ValidationContext(campsite);
      var actual = Validator.TryValidateObject(campsite, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Campsites))]
    public void Test_Validate_CampsiteModel(CampsiteModel campsite)
    {
      var validationContext = new ValidationContext(campsite);

      Assert.Empty(campsite.Validate(validationContext));
    }
  }
}
