namespace SelectAndRepair.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using SelectAndRepair.Services.Data.Categories;
    using SelectAndRepair.Services.Data.Organizations;
    using SelectAndRepair.Web.ViewModels.Categories;
    using SelectAndRepair.Web.ViewModels.Organizations;
    using Microsoft.AspNetCore.Mvc;

    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationsService organizationsService;
        private readonly ICategoriesService categoriesService;

        public OrganizationsController(
            IOrganizationsService organizationsService,
            ICategoriesService categoriesService)
        {
            this.organizationsService = organizationsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new OrganizationsByUserListViewModel
            {
                Organizations = await this.organizationsService.GetAllAsync<BaseOrganizationViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.organizationsService.GetByIdAsync<OrganizationWithServicesViewModel>(id);

            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            return this.View(viewModel);
        }
    }
}
