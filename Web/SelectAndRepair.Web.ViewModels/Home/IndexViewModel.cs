namespace SelectAndRepair.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexBlogPostsViewModel> BlogPosts { get; set; }
    }
}
