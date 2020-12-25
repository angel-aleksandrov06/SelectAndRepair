namespace SelectAndRepair.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;

    public class CategoryInputModel
    {
        [Required]
        [StringLength(
            DataValidations.NameMaxLength,
            ErrorMessage = ErrorMessages.Name,
            MinimumLength = DataValidations.NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            DataValidations.DescriptionMaxLength,
            ErrorMessage = ErrorMessages.Description,
            MinimumLength = DataValidations.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
