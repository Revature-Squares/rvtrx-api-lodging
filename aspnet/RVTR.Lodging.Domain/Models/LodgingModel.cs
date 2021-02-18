using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Attributes;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _campground_ model
  /// </summary>
  public class CampgroundModel : IValidatableObject
  {
    /// <summary>
    /// id of the campground model in the db
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Address id of the campground's location
    /// </summary>
    /// <value></value>
    public int AddressId { get; set; }

    /// <summary>
    /// Address property of the campground model (required)
    /// </summary>
    /// <value></value>
    public AddressModel Address { get; set; }

    /// <summary>
    /// Name of the campground (required)
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
    public string Name { get; set; }

    /// <summary>
    /// Number of bathrooms at the campground has to be one can have as any amount
    /// </summary>
    /// <value></value>
    [FacilitiesAttribute]
    public int Bathrooms { get; set; }


    /// <summary>
    /// Campsite list of the campground
    /// </summary>
    /// <value></value>
    public IEnumerable<CampsiteModel> Campsites { get; set; } = new List<CampsiteModel>();

    /// <summary>
    /// Review list for the campground
    /// </summary>
    /// <value></value>
    public IEnumerable<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();

    /// <summary>
    /// Review list for the images
    /// </summary>
    /// <value></value>
    public IEnumerable<ImageModel> Images { get; set; } = new List<ImageModel>();

    /// <summary>
    /// Represents the _Campground_ `Validate` model
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
