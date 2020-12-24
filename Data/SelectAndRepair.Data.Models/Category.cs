namespace SelectAndRepair.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;
    using SelectAndRepair.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Services = new HashSet<Service>();
            this.Organizations = new HashSet<Organization>();
        }

        [Required]
        [MaxLength(DataValidations.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DataValidations.DescriptionMaxLength)]
        public string Description { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
