namespace SelectAndRepair.Web.ViewModels.Organizations
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class OrganizationServiceViewModel : IMapFrom<OrganizationService>
    {
        public string OrganizationId { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string ServiceDescription { get; set; }

        public bool Available { get; set; }
    }
}
