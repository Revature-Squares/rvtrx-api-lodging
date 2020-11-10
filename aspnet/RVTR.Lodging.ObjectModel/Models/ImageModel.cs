using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  public class ImageModel : IValidatableObject
  {
    public int Id { get; set; }

    public int? LodgingModelId { get; set; }

    public string ImageUri { get; set; } = "https://bulma.io/images/placeholders/1280x960.png";

    /// <summary>
    /// Represents the _Rental_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
