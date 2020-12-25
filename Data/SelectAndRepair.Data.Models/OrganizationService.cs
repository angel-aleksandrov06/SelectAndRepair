namespace SelectAndRepair.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SelectAndRepair.Data.Common.Models;

    public class OrganizationService : BaseDeletableModel<int>
    {
        public OrganizationService()
        {
            this.Appointments = new HashSet<Appointment>();
        }

        [Required]
        public string OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public bool Available { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
