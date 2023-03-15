using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelectAndRepair.Data.Models;
using SelectAndRepair.Common;
using SelectAndRepair.Data.Common.Repositories;
using SelectAndRepair.Services.Data.Organizations;
using System.Threading.Tasks;
using SelectAndRepair.Web.ViewModels.Organizations;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SelectAndRepair.Web.ViewModels.Categories;
using SelectAndRepair.Web.ViewModels.Cities;
using SelectAndRepair.Services.Data.Categories;
using SelectAndRepair.Services.Data.Cities;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectAndRepair.Services.Data.Services;
using SelectAndRepair.Services.Data.OrganizationsServices;

namespace SelectAndRepair.Web.Areas.Organization.Controllers
{
    [Authorize(Roles = GlobalConstants.OrganizationRoleName)]
    public class DashboardController : OrganizationController
    {
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly ICitiesService citiesService;
        private readonly IServicesService servicesService;
        private readonly IOrganizationsServicesService organizationsServicesService;

        public DashboardController(IOrganizationsService organizationsService, 
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            ICitiesService citiesService,
            IServicesService servicesService,
            IOrganizationsServicesService organizationsServicesService)
        {
            this.organizationsService = organizationsService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.citiesService = citiesService;
            this.servicesService = servicesService;
            this.organizationsServicesService = organizationsServicesService;
        }

        public async Task<IActionResult> MyOrganizations()
        {
            var user = await userManager.GetUserAsync(this.User);
            var viewModel = new OrganizationsListViewModel
            {
                Organizations = await this.organizationsService.GetAllByOwnerIdAsync<OrganizationViewModel>(user.Id)
            }; 
            return View(viewModel);
        }

        public async Task<IActionResult> CreateOrganization()
        {
            var categories = await this.categoriesService.GetAllAsync<CategorySelectListViewModel>();
            var cities = await this.citiesService.GetAllAsync<CitySelectListViewModel>();

            this.ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            this.ViewData["Cities"] = new SelectList(cities, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrganization(OrganizationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            // Add Organization
            var organizationId = await this.organizationsService.AddAsync(input.Name, input.CategoryId, input.CityId, input.Address, input.ImageUrl, user);

            // Add to the Organization all Services from its Category
            var servicesIds = await this.servicesService.GetAllIdsByCategoryAsync(input.CategoryId);
            await this.organizationsServicesService.AddAsync(organizationId, servicesIds);

            return this.RedirectToAction("MyOrganizations");
        }

        public async Task<IActionResult> UpdateOrganization(string id)
        {
            var categories = await this.categoriesService.GetAllAsync<CategorySelectListViewModel>();
            var cities = await this.citiesService.GetAllAsync<CitySelectListViewModel>();

            this.ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            this.ViewData["Cities"] = new SelectList(cities, "Id", "Name");
            this.ViewData["OrganizationId"] = id;
            var model = await this.organizationsService.GetByIdAsync<OrganizationWithServicesViewModel>(id);
            var newViewModel = new OrganizationInputModel
            {
                Name = model.Name,
                Address = model.Address,
                CategoryId = categories.Where(x => x.Name == model.CategoryName).Select(x => x.Id).FirstOrDefault(),
                CityId = cities.Where(x => x.Name == model.CityName).Select(x => x.Id).FirstOrDefault(),
                ImageUrl = model.ImageUrl
            };

            return this.View(newViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateOrganization(string organizationId, OrganizationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.organizationsService.UpdateAsync(organizationId, input.Name, input.CategoryId, input.CityId, input.Address, input.ImageUrl);

            return this.RedirectToAction("MyOrganizations");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrganization(string id)
        {
            await this.organizationsService.DeleteAsync(id);

            return this.RedirectToAction("MyOrganizations");
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.organizationsService.GetByIdAsync<OrganizationWithServicesViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeServiceAvailableStatus(string organizationId, int serviceId)
        {
            await this.organizationsServicesService.ChangeAvailableStatusAsync(organizationId, serviceId);

            return this.RedirectToAction("Details", new { id = organizationId });
        }
    }
}
