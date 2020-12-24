namespace SelectAndRepair.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;
    using SelectAndRepair.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Organizations = new HashSet<Organization>();
        }

        [Required]
        [MaxLength(DataValidations.NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
