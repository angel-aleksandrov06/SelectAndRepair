namespace SelectAndRepair.Data.Models
{

    using SelectAndRepair.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Value { get; set; }
    }
}
