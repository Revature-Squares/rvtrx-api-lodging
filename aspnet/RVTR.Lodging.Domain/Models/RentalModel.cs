using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Rental_ model
  /// </summary>
  public class RentalModel : IValidatableObject
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Lot number can't be null.")]
    [MaxLength(10, ErrorMessage = "Lot number must be 10 digits maximum")]
    [RegularExpression(@"^\d+([a-zA-Z]+)?$", ErrorMessage = "Lot number must be either a number or a number plus a series of letters.")]
    public string LotNumber { get; set; }

    [Required(ErrorMessage = "Status can't be null.")]
    [RegularExpression(@"^([Bb]ooked|[Aa]vailable)$", ErrorMessage = "Status must be either 'Booked' or 'Available'")]
    public string Status { get; set; }

    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double Price { get; set; }

    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double? DiscountedPrice { get; set; }

    public int? LodgingModelId { get; set; }


    //---------------------------------------------------------------------------------------------
    // Start of RentalUnitModel

    /// <summary>
    /// The capacity of the rental unit
    /// </summary>
    /// <value></value>
    [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000")]
    public int Capacity { get; set; }

    /// <summary>
    /// The name of the rental unit
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Name must exist.")]
    [MaxLength(100, ErrorMessage = "Name must be fewer than 100 characters")]
    public string SiteName { get; set; }

    /// <summary>
    /// Id of the rental
    /// </summary>
    /// <value></value>
    public RentalModel Rental { get; set; }

    /// <summary>
    /// the size of the rental unit (e.g. 5 x 5, 5x5, 5ft x 5ft, 5 yards x 5 yards etc.)
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Size must exist")]
    [RegularExpression(@"^\d+ ?([Ff]t|[Yy]ards|[Mm]eters|[Mm]) ?x ?\d+ ?([Ff]t|[Yy]ards|[Mm]eters|[Mm])$", ErrorMessage = "Size must be in the form '10 [unit?] x 10 [unit?]'")]
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
