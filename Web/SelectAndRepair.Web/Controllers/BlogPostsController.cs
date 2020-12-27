namespace SelectAndRepair.Web.Controllers
{
    using System.Threading.Tasks;

    using SelectAndRepair.Common;
    using SelectAndRepair.Services.Data.BlogPosts;
    using SelectAndRepair.Web.ViewModels.BlogPosts;
    using Microsoft.AspNetCore.Mvc;

    public class BlogPostsController : BaseController
    {
        private readonly IBlogPostsService blogPostsService;

        public BlogPostsController(IBlogPostsService blogPostsService)
        {
            this.blogPostsService = blogPostsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new BlogPostsListViewModel
            {
                BlogPosts = await this.blogPostsService.GetAllAsync<BlogPostViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
