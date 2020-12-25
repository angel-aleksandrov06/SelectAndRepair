namespace SelectAndRepair.Web.ViewModels.Organizations
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class OrganizationViewModel : IMapFrom<Organization>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CityName { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }

        public int AppointmentsCount { get; set; }
    }
}
