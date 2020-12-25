namespace SelectAndRepair.Web.ViewModels.Cities
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class CityViewModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OrganizationsCount { get; set; }
    }
}
