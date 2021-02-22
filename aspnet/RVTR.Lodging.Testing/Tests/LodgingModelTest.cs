using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class LodgingModelTest
  {
    public static readonly IEnumerable<object[]> Lodgings = new List<object[]>
    {
      new object[]
      {
        new LodgingModel
        {
          Id = 0,
          Address = new AddressModel(),
          Name = "Name",
          Bathrooms = 1,
          Rentals = new List<RentalModel>(),
          Reviews = new List<ReviewModel>()
        }
      }
    };

    [Theory]
    [MemberData(nameof(Lodgings))]
    public void Test_Create_LodgingModel(LodgingModel lodging)
    {
      var validationContext = new ValidationContext(lodging);
      var actual = Validator.TryValidateObject(lodging, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Lodgings))]
    public void Test_Validate_LodgingModel(LodgingModel lodging)
    {
      var validationContext = new ValidationContext(lodging);

      Assert.Empty(lodging.Validate(validationContext));
    }
  }
}
