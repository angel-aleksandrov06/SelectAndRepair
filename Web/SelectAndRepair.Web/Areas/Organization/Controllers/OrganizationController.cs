namespace SelectAndRepair.Web.Areas.Organization.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SelectAndRepair.Common;
    using SelectAndRepair.Web.Controllers;

    [Authorize(Roles = GlobalConstants.OrganizationRoleName)]
    [Area("Organization")]
    public abstract class OrganizationController : BaseController
    {
    }
}
