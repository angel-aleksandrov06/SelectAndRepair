namespace SelectAndRepair.Web.Areas.Administration.Controllers
{
    using SelectAndRepair.Common;
    using SelectAndRepair.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
