using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Attributes;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Rental_ model
  /// </summary>
  public class RentalModel : IValidatableObject
  {
    public int Id { get; set; }

    [LotNumberAttribute]
    public string LotNumber { get; set; }

    [StatusAttribute]
    public string Status { get; set; }

    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double Price { get; set; }

    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double? DiscountedPrice { get; set; }

    public int? LodgingModelId { get; set; }


    //---------------------------------------------------------------------------------------------
    // Start of RentalUnitModel

    /// <summary>
    /// How many people the campsite can host.
    /// </summary>
    /// <value></value>
    [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000")]
    public int CapacityPeople { get; set; }

    /// <summary>
    /// The number of parking spaces at this campsite.
    /// </summary>
    /// <value></value>
    [Range(0, 100, ErrorMessage = "Capacity must be between 0 and 100")]
    public int CapacityVehicles { get; set; }

    /// <summary>
    /// The name of the campsite.
    /// </summary>
    /// <value></value>
    [SiteNameAttribute]
    public string SiteName { get; set; }

    /// <summary>
    /// Id of the Campground
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Campsites must be part of a campground.")]
    public LodgingModel Campground { get; set; }

    /// <summary>
    /// the size of the rental unit (e.g. 5 x 5, 5x5, 5ft x 5ft, 5 yards x 5 yards etc.)
    /// </summary>
    /// <value></value>
    [SizeAttribute]
    public string Size { get; set; }

    /// <summary>
    /// enum of the possible types for the campsite.
    /// </summary>
    /// <value></value>
    public enum CampsiteType
    {
      Glamping = 0,
      RV,
      CarCamping,
      Backpacking
    }

<<<<<<< HEAD
    /// <summary>
    /// type of this campsite.
    /// </summary>
    /// <value></value>
=======
>>>>>>> added [Required] Type enum property to RentalModel in aspnet Domain
    [Required(ErrorMessage = "Must specify the type of campsite ('Glamping', 'RV', 'CarCamping', or 'Backpacking').")]
    public CampsiteType Type {get;set;}

    /// <summary>
    /// Represents the _Rental_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
