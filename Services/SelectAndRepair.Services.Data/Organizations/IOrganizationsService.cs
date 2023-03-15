namespace SelectAndRepair.Services.Data.Organizations
{
    using SelectAndRepair.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrganizationsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<string>> GetAllIdsByCategoryAsync(int categoryId);

        Task<T> GetByIdAsync<T>(string id);

        Task<string> AddAsync(string name, int categoryId, int cityId, string address, string imageUrl, ApplicationUser owner = null);

        Task UpdateAsync(string id, string name, int categoryId, int cityId, string address, string imageUrl);

        Task DeleteAsync(string id);

        Task<string> GetOrganizationEmail(string organizationId);

        Task<IEnumerable<T>> GetAllByOwnerIdAsync<T>(string ownerId);
    }
}
