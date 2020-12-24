namespace SelectAndRepair.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;
    using SelectAndRepair.Data.Common.Models;

    public class Service : BaseDeletableModel<int>
    {
        public Service()
        {
            this.Appointments = new HashSet<Appointment>();
            this.Organizations = new HashSet<OrganizationService>();
        }

        [Required]
        [MaxLength(DataValidations.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataValidations.DescriptionMaxLength)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<OrganizationService> Organizations { get; set; }
    }
}
