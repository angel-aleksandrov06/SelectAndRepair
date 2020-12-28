namespace SelectAndRepair.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task AddAsync(string name, string description, string imageUrl);

        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);

        Task DeleteAsync(int id);
    }
}
