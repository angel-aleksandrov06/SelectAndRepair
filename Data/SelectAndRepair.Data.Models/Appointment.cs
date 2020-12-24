namespace SelectAndRepair.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Data.Common.Models;

    public class Appointment : BaseDeletableModel<string>
    {
        public DateTime DateTime { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public int OrganizationServiceId { get; set; }

        public virtual OrganizationService OrganizationService { get; set; }

        public bool? Confirmed { get; set; }

        public bool? IsOrganizationRatedByTheUser { get; set; }
    }
}
