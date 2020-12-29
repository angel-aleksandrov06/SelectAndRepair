namespace SelectAndRepair.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Data.OrganizationsServices;
    using Xunit;

    public class OrganizationsServicesServiceTests : BaseServiceTests
    {
        private IOrganizationsServicesService Service => this.ServiceProvider.GetRequiredService<IOrganizationsServicesService>();

        [Fact]
        public async Task AddAsyncShouldWorkCorrectlyWithOneOrganizationAndManyServices()
        {
            var organization = await this.CreateOrganizationAsync();
            var service1 = await this.CreateServiceAsync();
            var service2 = await this.CreateServiceAsync();
            var service3 = await this.CreateServiceAsync();

            var organizationId = organization.Id;
            var servicesIds = new List<int> { service1.Id, service2.Id, service3.Id };

            await this.Service.AddAsync(organizationId, servicesIds);

            var expected = 3;
            var actual = await this.DbContext.OrganizationServices.CountAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddAsyncShouldWorkCorrectlyWithManyOrganizationsAndOneService()
        {
            var organization1 = await this.CreateOrganizationAsync();
            var organization2 = await this.CreateOrganizationAsync();
            var organization3 = await this.CreateOrganizationAsync();
            var service = await this.CreateServiceAsync();

            var organizationsIds = new List<string> { organization1.Id, organization2.Id, organization3.Id };
            var serviceId = service.Id;

            await this.Service.AddAsync(organizationsIds, serviceId);

            var expected = 3;
            var actual = await this.DbContext.OrganizationServices.CountAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ChangeAvailableStatusAsyncShouldChangeStatusCorrectly()
        {
            var organization = await this.CreateOrganizationAsync();
            var service = await this.CreateServiceAsync();
            var organizationService = await this.CreateOrganizationServiceAsync(organization.Id, service.Id);

            await this.Service.ChangeAvailableStatusAsync(organization.Id, service.Id);

            Assert.True(organizationService.Available);
        }

        private async Task<Organization> CreateOrganizationAsync()
        {
            // Add Organization
            var organization = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = new NLipsum.Core.Sentence().ToString(),
                CategoryId = 1,
                CityId = 1,
                Address = new NLipsum.Core.Sentence().ToString(),
                ImageUrl = new NLipsum.Core.Word().ToString(),
                Rating = 4,
                RatersCount = 1,
            };

            await this.DbContext.Organizations.AddAsync(organization);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Organization>(organization).State = EntityState.Detached;

            return organization;
        }

        private async Task<Service> CreateServiceAsync()
        {
            var service = new Service
            {
                Name = new NLipsum.Core.Sentence().ToString(),
                Description = new NLipsum.Core.Paragraph().ToString(),
                CategoryId = 1,
            };

            await this.DbContext.Services.AddAsync(service);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<Service>(service).State = EntityState.Detached;

            return service;
        }

        private async Task<OrganizationService> CreateOrganizationServiceAsync(string organizationId, int serviceId)
        {
            var organizationService = new OrganizationService
            {
                OrganizationId = organizationId,
                ServiceId = serviceId,
                Available = true,
            };

            await this.DbContext.OrganizationServices.AddAsync(organizationService);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Entry<OrganizationService>(organizationService).State = EntityState.Detached;

            return organizationService;
        }
    }
}
