namespace SelectAndRepair.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SelectAndRepair.Common;
    using SelectAndRepair.Services.Data.Categories;
    using SelectAndRepair.Services.Data.Organizations;
    using SelectAndRepair.Services.Data.OrganizationsServices;
    using SelectAndRepair.Services.Data.Services;
    using SelectAndRepair.Web.ViewModels.Categories;
    using SelectAndRepair.Web.ViewModels.Services;

    public class ServicesController : AdministrationController
    {
        private readonly IServicesService servicesService;
        private readonly ICategoriesService categoriesService;
        private readonly IOrganizationsService organizationsService;
        private readonly IOrganizationsServicesService organizationsServicesService;

        public ServicesController(
            IServicesService servicesService,
            ICategoriesService categoriesService,
            IOrganizationsService organizationsService,
            IOrganizationsServicesService organizationsServicesService)
        {
            this.servicesService = servicesService;
            this.categoriesService = categoriesService;
            this.organizationsService = organizationsService;
            this.organizationsServicesService = organizationsServicesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ServicesListViewModel
            {
                Services = await this.servicesService.GetAllAsync<ServiceViewModel>(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> AddService()
        {
            var categories = await this.categoriesService.GetAllAsync<CategorySelectListViewModel>();
            this.ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(ServiceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // Add Service
            var serviceId = await this.servicesService.AddAsync(input.Name, input.CategoryId, input.Description);

            // Add the Service to all organizations in the Category
            var organizationsIds = await this.organizationsService.GetAllIdsByCategoryAsync(input.CategoryId);
            await this.organizationsServicesService.AddAsync(organizationsIds, serviceId);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (id <= SeededDataCounts.Services)
            {
                return this.RedirectToAction("Index");
            }

            await this.servicesService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}
