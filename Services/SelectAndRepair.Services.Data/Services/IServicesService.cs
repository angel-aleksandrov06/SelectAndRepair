namespace SelectAndRepair.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServicesService
    {
        Task<int> AddAsync(string name, int categoryId, string description);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<int>> GetAllIdsByCategoryAsync(int categoryId);
    }
}
