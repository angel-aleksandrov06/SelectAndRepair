namespace SelectAndRepair.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SelectAndRepair.Data.Common.Repositories;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        public ServicesService(IDeletableEntityRepository<Service> servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public async Task<int> AddAsync(string name, int categoryId, string description)
        {
            var service = new Service
            {
                Name = name,
                CategoryId = categoryId,
                Description = description,
            };

            await this.servicesRepository.AddAsync(service);
            await this.servicesRepository.SaveChangesAsync();

            return service.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var service =
                await this.servicesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            this.servicesRepository.Delete(service);
            await this.servicesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.servicesRepository
                .All()
                .OrderBy(x => x.CategoryId)
                .ThenBy(x => x.Id)
                .To<T>().ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllIdsByCategoryAsync(int categoryId)
        {
            return await this.servicesRepository
                .All()
                .Where(x => x.CategoryId == categoryId)
                .OrderBy(x => x.Id)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
