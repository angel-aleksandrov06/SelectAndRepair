namespace SelectAndRepair.Services.Data.Cities
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICitiesService
    {
        Task AddAsync(string name);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
