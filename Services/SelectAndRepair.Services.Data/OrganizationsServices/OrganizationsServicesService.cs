namespace SelectAndRepair.Services.Data.OrganizationsServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SelectAndRepair.Data.Common.Repositories;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class OrganizationsServicesService : IOrganizationsServicesService
    {
        private readonly IDeletableEntityRepository<OrganizationService> organizationServicesRepository;

        public OrganizationsServicesService(IDeletableEntityRepository<OrganizationService> organizationServicesRepository)
        {
            this.organizationServicesRepository = organizationServicesRepository;
        }

        public async Task AddAsync(string organizationId, IEnumerable<int> servicesIds)
        {
            foreach (var serviceId in servicesIds)
            {
                var organizationsService = new OrganizationService
                {
                    OrganizationId = organizationId,
                    ServiceId = serviceId,
                    Available = true,
                };

                await this.organizationServicesRepository.AddAsync(organizationsService);
            }

            await this.organizationServicesRepository.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<string> organizationsIds, int serviceId)
        {
            foreach (var organizationId in organizationsIds)
            {
                var organizationsService = new OrganizationService
                {
                    OrganizationId = organizationId,
                    ServiceId = serviceId,
                    Available = true,
                };

                await this.organizationServicesRepository.AddAsync(organizationsService);
            }

            await this.organizationServicesRepository.SaveChangesAsync();
        }

        public async Task ChangeAvailableStatusAsync(string organizationId, int serviceId)
        {
            var organizationService =
                await this.organizationServicesRepository
                .All()
                .Where(x => x.OrganizationId == organizationId
                            && x.ServiceId == serviceId)
                .FirstOrDefaultAsync();

            organizationService.Available = !organizationService.Available;

            await this.organizationServicesRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(string organizationId, int serviceId)
        {
            return await this.organizationServicesRepository
                .All()
                .Where(x => x.OrganizationId == organizationId && x.ServiceId == serviceId)
                .To<T>().FirstOrDefaultAsync();
        }
    }
}
