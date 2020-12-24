namespace SelectAndRepair.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Common;
    using SelectAndRepair.Data.Common.Models;

    public class Organization : BaseDeletableModel<string>
    {
        public Organization()
        {
            this.Appointments = new HashSet<Appointment>();
            this.OrganizationService = new HashSet<OrganizationService>();
        }

        [Required]
        [MaxLength(DataValidations.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        [MaxLength(DataValidations.AddressMaxLength)]
        public string Address { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }

        public virtual ICollection<OrganizationService> OrganizationService { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
