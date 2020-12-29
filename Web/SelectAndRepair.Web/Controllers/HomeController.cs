namespace SelectAndRepair.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SelectAndRepair.Services.Data.BlogPosts;
    using SelectAndRepair.Services.Data.Categories;
    using SelectAndRepair.Web.ViewModels;
    using SelectAndRepair.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IBlogPostsService blogPosts;

        public HomeController(ICategoriesService categoriesService, IBlogPostsService blogPosts)
        {
            this.categoriesService = categoriesService;
            this.blogPosts = blogPosts;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                BlogPosts = await this.blogPosts
                .GetAllAsync<IndexBlogPostsViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
