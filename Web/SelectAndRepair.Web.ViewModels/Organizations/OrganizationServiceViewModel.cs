namespace SelectAndRepair.Web.ViewModels.Organizations
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class OrganizationServiceViewModel : IMapFrom<OrganizationService>
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public bool Available { get; set; }
    }
}
