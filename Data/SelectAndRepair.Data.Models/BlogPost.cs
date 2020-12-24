namespace SelectAndRepair.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;
    using SelectAndRepair.Data.Common.Models;

    public class BlogPost : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(DataValidations.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DataValidations.NameMaxLength)]
        public string Author { get; set; }

        [Required]
        [MaxLength(DataValidations.ContentMaxLength)]
        public string Content { get; set; }
    }
}
