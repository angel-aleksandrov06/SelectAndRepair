namespace SelectAndRepair.Web.ViewModels.Home
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
