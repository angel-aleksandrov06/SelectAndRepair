namespace SelectAndRepair.Web.ViewModels.Organizations
{
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;

    public class OrganizationInputModel
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
        public int CityId { get; set; }

        [Required]
        [StringLength(
            DataValidations.AddressMaxLength,
            ErrorMessage = ErrorMessages.Address,
            MinimumLength = DataValidations.AddressMinLength)]
        public string Address { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
