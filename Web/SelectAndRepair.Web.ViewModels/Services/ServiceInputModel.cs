namespace SelectAndRepair.Web.ViewModels.Services
{
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;

    public class ServiceInputModel
    {
        [Required]
        [StringLength(
            DataValidations.NameMaxLength,
            ErrorMessage = ErrorMessages.Name,
            MinimumLength = DataValidations.NameMinLength)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(
            DataValidations.DescriptionMaxLength,
            ErrorMessage = ErrorMessages.Description,
            MinimumLength = DataValidations.DescriptionMinLength)]
        public string Description { get; set; }
    }
}
