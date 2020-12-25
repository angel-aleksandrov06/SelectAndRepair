namespace SelectAndRepair.Services.Data.Organizations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SelectAndRepair.Data.Common.Repositories;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class OrganizationsService : IOrganizationsService
    {
        private readonly IDeletableEntityRepository<Organization> organizationRepository;

        public OrganizationsService(IDeletableEntityRepository<Organization> organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.organizationRepository
                .All()
                .OrderBy(x => x.Name)
                .To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSortingFilteringAndPagingAsync<T>(
            string searchString,
            int? sortId,
            int pageSize,
            int pageIndex)
        {
            IQueryable<Organization> query =
                this.organizationRepository
                .AllAsNoTracking()
                .OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query
                    .Where(x => x.Name.ToLower()
                    .Contains(searchString.ToLower()));
            }

            if (sortId != null)
            {
                query = query
                    .Where(x => x.CategoryId == sortId);
            }

            return await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .To<T>().ToListAsync();
        }

        public async Task<int> GetCountForPaginationAsync(string searchString, int? sortId)
        {
            IQueryable<Organization> query =
                this.organizationRepository
                .AllAsNoTracking()
                .OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query
                    .Where(x => x.Name.ToLower()
                                .Contains(searchString.ToLower()));
            }

            if (sortId != null)
            {
                query = query
                    .Where(x => x.CategoryId == sortId);
            }

            return await query.CountAsync();
        }

        public async Task<IEnumerable<string>> GetAllIdsByCategoryAsync(int categoryId)
        {
            return await this.organizationRepository
                .All()
                .Where(x => x.CategoryId == categoryId)
                .Select(x => x.Id)
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            return await this.organizationRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();
        }

        public async Task<string> AddAsync(string name, int categoryId, int cityId, string address, string imageUrl)
        {
            var organization = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                CategoryId = categoryId,
                CityId = cityId,
                Address = address,
                ImageUrl = imageUrl,
                Rating = 0,
                RatersCount = 0,
            };

            await this.organizationRepository.AddAsync(organization);
            await this.organizationRepository.SaveChangesAsync();

            return organization.Id;
        }

        public async Task DeleteAsync(string id)
        {
            var organization =
                await this.organizationRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            this.organizationRepository.Delete(organization);
            await this.organizationRepository.SaveChangesAsync();
        }
    }
}
