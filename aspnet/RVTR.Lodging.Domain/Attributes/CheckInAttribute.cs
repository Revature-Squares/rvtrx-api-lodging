using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Attributes
{
  public class CheckInAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      return (bool)value;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var checkIn = (bool)value;

      return (checkIn) ? ValidationResult.Success : new ValidationResult("User needs to be checked in.");
    }
  }
}
