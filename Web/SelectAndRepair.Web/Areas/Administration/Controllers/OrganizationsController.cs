namespace SelectAndRepair.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SelectAndRepair.Services.Data.Categories;
    using SelectAndRepair.Services.Data.Cities;
    using SelectAndRepair.Services.Data.Organizations;
    using SelectAndRepair.Services.Data.OrganizationsServices;
    using SelectAndRepair.Services.Data.Services;
    using SelectAndRepair.Web.ViewModels.Categories;
    using SelectAndRepair.Web.ViewModels.Cities;
    using SelectAndRepair.Web.ViewModels.Organizations;

    public class OrganizationsController : AdministrationController
    {
        private readonly IOrganizationsService organizationsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICitiesService citiesService;
        private readonly IServicesService servicesService;
        private readonly IOrganizationsServicesService organizationsServicesService;

        public OrganizationsController(
            IOrganizationsService organizationsService,
            ICategoriesService categoriesService,
            ICitiesService citiesService,
            IServicesService servicesService,
            IOrganizationsServicesService organizationsServicesService)
        {
            this.organizationsService = organizationsService;
            this.categoriesService = categoriesService;
            this.citiesService = citiesService;
            this.servicesService = servicesService;
            this.organizationsServicesService = organizationsServicesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new OrganizationsListViewModel
            {
                Organizations = await this.organizationsService.GetAllAsync<OrganizationViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddOrganization()
        {
            var categories = await this.categoriesService.GetAllAsync<CategorySelectListViewModel>();
            var cities = await this.citiesService.GetAllAsync<CitySelectListViewModel>();

            this.ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            this.ViewData["Cities"] = new SelectList(cities, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrganization(OrganizationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // Add Organization
            var organizationId = await this.organizationsService.AddAsync(input.Name, input.CategoryId, input.CityId, input.Address, input.ImageUrl);

            // Add to the Organization all Services from its Category
            var servicesIds = await this.servicesService.GetAllIdsByCategoryAsync(input.CategoryId);
            await this.organizationsServicesService.AddAsync(organizationId, servicesIds);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrganization(string id)
        {
            if (id.StartsWith("seeded"))
            {
                return this.RedirectToAction("Index");
            }

            await this.organizationsService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}
