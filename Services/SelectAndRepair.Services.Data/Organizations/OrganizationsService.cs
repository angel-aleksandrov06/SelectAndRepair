﻿namespace SelectAndRepair.Services.Data.Organizations
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

        public async Task<string> AddAsync(string name, int categoryId, int cityId, string address, string imageUrl, ApplicationUser owner = null)
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

            if (owner != null)
            {
                organization.Owner = owner;
            }

            await this.organizationRepository.AddAsync(organization);
            await this.organizationRepository.SaveChangesAsync();

            return organization.Id;
        }

        public async Task UpdateAsync(string id, string name, int categoryId, int cityId, string address, string imageUrl)
        {
            var organization = this.organizationRepository.All().FirstOrDefault(x => x.Id == id);

            organization.Name = name;
            organization.CategoryId = categoryId;
            organization.CityId = cityId;
            organization.Address = address;
            organization.ImageUrl = imageUrl;

            this.organizationRepository.Update(organization);
            await this.organizationRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var organization = await this.organizationRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            this.organizationRepository.Delete(organization);
            await this.organizationRepository.SaveChangesAsync();
        }

        public async Task<string> GetOrganizationEmail(string organizationId)
        {
            var organizationEmail = await organizationRepository
                .AllAsNoTracking()
                .Include(x => x.Owner)
                .Where(x => x.Id == organizationId)
                .Select(x => x.Owner.Email)
                .FirstOrDefaultAsync();

            return organizationEmail;
        }

        public async Task<IEnumerable<T>> GetAllByOwnerIdAsync<T>(string ownerId)
        {
            return await this.organizationRepository
                .All()
                .Where(x => x.OwnerId == ownerId)
                .OrderBy(x => x.Name)
                .To<T>().ToListAsync();
        }
    }
}
