namespace SelectAndRepair.Web.ViewModels.BlogPosts
{
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;

    public class BlogPostInputModel
    {
        [Required]
        [StringLength(
            DataValidations.TitleMaxLength,
            ErrorMessage = ErrorMessages.Title,
            MinimumLength = DataValidations.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            DataValidations.ContentMaxLength,
            ErrorMessage = ErrorMessages.Content,
            MinimumLength = DataValidations.ContentMinLength)]
        public string Content { get; set; }

        [Required]
        [StringLength(
            DataValidations.NameMaxLength,
            ErrorMessage = ErrorMessages.Author,
            MinimumLength = DataValidations.NameMinLength)]
        public string Author { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
