namespace SelectAndRepair.Web.ViewModels.Categories
{
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class CategorySelectListViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
