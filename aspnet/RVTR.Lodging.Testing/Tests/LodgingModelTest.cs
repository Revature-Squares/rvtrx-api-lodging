using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class CampgroundModelTest
  {
    public static readonly IEnumerable<object[]> Campgrounds = new List<object[]>
    {
      new object[]
      {
        new CampgroundModel
        {
          Id = 0,
          Address = new AddressModel(),
          Name = "Name",
          Bathrooms = 1,
          Campsites = new List<CampsiteModel>(),
          Reviews = new List<ReviewModel>()
        }
      }
    };

    [Theory]
    [MemberData(nameof(Campgrounds))]
    public void Test_Create_CampgroundModel(CampgroundModel campground)
    {
      var validationContext = new ValidationContext(campground);
      var actual = Validator.TryValidateObject(campground, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Campgrounds))]
    public void Test_Validate_CampgroundModel(CampgroundModel campground)
    {
      var validationContext = new ValidationContext(campground);

      Assert.Empty(campground.Validate(validationContext));
    }
  }
}
