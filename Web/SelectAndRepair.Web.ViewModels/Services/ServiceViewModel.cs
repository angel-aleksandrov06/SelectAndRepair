namespace SelectAndRepair.Web.ViewModels.Services
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class ServiceViewModel : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int OrganizationsCount { get; set; }

        public int AppointmentsCount { get; set; }
    }
}
