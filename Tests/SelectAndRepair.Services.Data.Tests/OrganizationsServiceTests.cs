namespace SelectAndRepair.Services.Data.Tests.UseInMemoryDatabase
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Data.Organizations;
    using Xunit;

    public class OrganizationsServiceTests : BaseServiceTests
    {
        private IOrganizationsService Service => this.ServiceProvider.GetRequiredService<IOrganizationsService>();

        /*
        TODO: Task<IEnumerable<T>> GetAllAsync<T>();

        TODO: Task<T> GetByIdAsync<T>(string id);
         */
        [Fact]
        public async Task GetAllIdsByCategoryAsyncShouldReturnCorrectCount()
        {
            var organization1 = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = new NLipsum.Core.Sentence().ToString(),
                CategoryId = 5,
                CityId = 1,
                Address = new NLipsum.Core.Sentence().ToString(),
                ImageUrl = new NLipsum.Core.Word().ToString(),
                Rating = 4,
                RatersCount = 1,
            };
            var organization2 = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = new NLipsum.Core.Sentence().ToString(),
                CategoryId = 5,
                CityId = 1,
                Address = new NLipsum.Core.Sentence().ToString(),
                ImageUrl = new NLipsum.Core.Word().ToString(),
                Rating = 4,
                RatersCount = 1,
            };
            var organization3 = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = new NLipsum.Core.Sentence().ToString(),
                CategoryId = 5,
                CityId = 1,
                Address = new NLipsum.Core.Sentence().ToString(),
                ImageUrl = new NLipsum.Core.Word().ToString(),
                Rating = 4,
                RatersCount = 1,
            };

            await this.DbContext.Organizations.AddRangeAsync(organization1, organization2, organization3);
            await this.DbContext.SaveChangesAsync();

            var expected = this.DbContext.Organizations.Where(x => x.CategoryId == 5).Count();
            var actual = await this.Service.GetAllIdsByCategoryAsync(5);
            var actualCount = 0;

            foreach (var result in actual)
            {
                actualCount++;
            }

            Assert.Equal(expected, actualCount);
        }

        [Fact]
        public async Task AddAsyncShouldAddCorrectly()
        {
            var newGuidId = Guid.NewGuid().ToString();
            await this.CreateOrganizationAsync(newGuidId);

            var name = new NLipsum.Core.Sentence().ToString();
            var categoryId = 1;
            var cityId = 1;
            var address = new NLipsum.Core.Sentence().ToString();
            var imageUrl = new NLipsum.Core.Word().ToString();

            await this.Service.AddAsync(name, categoryId, cityId, address, imageUrl);

            var organizationsCount = await this.DbContext.Organizations.CountAsync();

            Assert.Equal(2, organizationsCount);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteCorrectly()
        {
            var newGuidId = Guid.NewGuid().ToString();

            var organization = await this.CreateOrganizationAsync(newGuidId);

            await this.Service.DeleteAsync(organization.Id);

            var organizationsCount = this.DbContext.Organizations.Where(x => !x.IsDeleted).ToArray().Count();
            var deletedOrganization = await this.DbContext.Organizations.FirstOrDefaultAsync(x => x.Id == organization.Id);
            Assert.Equal(0, organizationsCount);
            Assert.Null(deletedOrganization);
        }

        private async Task<Organization> CreateOrganizationAsync(string newGuidId)
        {
            var organization = new Organization
            {
                Id = newGuidId,
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
    }
}
