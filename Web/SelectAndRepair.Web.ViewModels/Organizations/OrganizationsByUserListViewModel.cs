namespace SelectAndRepair.Web.ViewModels.Organizations
{
    using System.Collections.Generic;

    public class OrganizationsByUserListViewModel
    {
        public IEnumerable<BaseOrganizationViewModel> Organizations { get; set; }
    }
}
