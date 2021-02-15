using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Attributes
{
  public class SiteNameAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var rentalModel = (RentalModel)validationContext.ObjectInstance;

        if (rentalModel.SiteName == null)
        {
            return new ValidationResult("SiteName can't be null.");
        }
        if (rentalModel.SiteName.Length >100)
        {
            return new ValidationResult("SiteName must be 100 characters maximum.");
        }
        return ValidationResult.Success;
    }
  }
}
