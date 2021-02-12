using System;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Attributes
{
  public class FacilitiesAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var NumberOf = (int) value;

      if( NumberOf > 1)
      {
        return ValidationResult.Success;
      }

      return new ValidationResult("Must have at least one facility");
    }
  }
}
