namespace SelectAndRepair.Web.ViewModels.Organizations
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class OrganizationViewModel : BaseOrganizationViewModel
    {
        public int CategoryId { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }

        public int AppointmentsCount { get; set; }
    }
}
