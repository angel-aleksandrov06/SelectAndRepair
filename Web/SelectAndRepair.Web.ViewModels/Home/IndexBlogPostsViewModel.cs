namespace SelectAndRepair.Web.ViewModels.Home
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class IndexBlogPostsViewModel : IMapFrom<BlogPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }
    }
}
