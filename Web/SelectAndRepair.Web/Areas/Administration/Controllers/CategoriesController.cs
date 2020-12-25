namespace SelectAndRepair.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SelectAndRepair.Common;
    using SelectAndRepair.Services.Data.Categories;
    using SelectAndRepair.Web.ViewModels.Categories;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CategoriesListViewModel
            {
                Categories = await this.categoriesService.GetAllAsync<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult AddCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.AddAsync(input.Name, input.Description, input.Image);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= SeededDataCounts.Categories)
            {
                return this.RedirectToAction("Index");
            }

            await this.categoriesService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}
