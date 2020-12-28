namespace SelectAndRepair.Services.Data.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SelectAndRepair.Data;
    using SelectAndRepair.Data.Common.Repositories;
    using SelectAndRepair.Data.Repositories;
    using SelectAndRepair.Services.Data.BlogPosts;
    using SelectAndRepair.Services.Data.Categories;
    using SelectAndRepair.Services.Data.Cities;
    using SelectAndRepair.Services.Data.Organizations;
    using SelectAndRepair.Services.Data.OrganizationsServices;
    using SelectAndRepair.Services.Data.Services;

    public abstract class BaseServiceTests : IDisposable
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient<IBlogPostsService, BlogPostsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IServicesService, ServicesService>();
            services.AddTransient<ICitiesService, CitiesService>();
            services.AddTransient<IOrganizationsService, OrganizationsService>();
            services.AddTransient<IOrganizationsServicesService, OrganizationsServicesService>();

            return services;
        }
    }
}
