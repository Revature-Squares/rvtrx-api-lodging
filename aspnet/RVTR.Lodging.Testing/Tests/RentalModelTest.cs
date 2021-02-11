using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class RentalModelTest
  {
    public static readonly IEnumerable<object[]> Rentals = new List<object[]>
    {
      new object[]
      {
        new RentalModel
        {
          Id = 0,
          Status = "Available",
          Price = 0.0,
          DiscountedPrice = 0.0,
          LodgingModelId = 0,
          LotNumber = "1",
<<<<<<< HEAD
          CapacityPeople = 1,
          CapacityVehicles = 0,
          SiteName = "TestCampsite",
          Campground = new LodgingModel(),
          Type = RentalModel.CampsiteType.Backpacking
=======
          Capacity = 1,
          SiteName = "TestCampsite",
          Campground = new LodgingModel(),
          Type = CampsiteType.Backpacking
>>>>>>> RentalModel had the Rental property, intended for a RentalUnitModel to refer to a RentalModel; changed to Campground property, referring to a LodgingModel, and made it [Required]. Updated RentalModelTest to include the new properties of RentalModel
        }
      }
    };

    [Theory]
    [MemberData(nameof(Rentals))]
    public void Test_Create_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);
      var actual = Validator.TryValidateObject(rental, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Rentals))]
    public void Test_Validate_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);

      Assert.Empty(rental.Validate(validationContext));
    }
  }
}
