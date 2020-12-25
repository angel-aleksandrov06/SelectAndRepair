namespace SelectAndRepair.Web.ViewModels.Cities
{
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;

    public class CityInputModel
    {
        [Required]
        [StringLength(
            DataValidations.NameMaxLength,
            ErrorMessage = ErrorMessages.Name,
            MinimumLength = DataValidations.NameMinLength)]
        public string Name { get; set; }
    }
}
