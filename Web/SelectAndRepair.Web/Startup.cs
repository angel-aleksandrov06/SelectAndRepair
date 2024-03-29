﻿namespace SelectAndRepair.Web
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SelectAndRepair.Data;
    using SelectAndRepair.Data.Common;
    using SelectAndRepair.Data.Common.Repositories;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Data.Repositories;
    using SelectAndRepair.Data.Seeding;
    using SelectAndRepair.Services.Data.BlogPosts;
    using SelectAndRepair.Services.Data.Categories;
    using SelectAndRepair.Services.Data.Cities;
    using SelectAndRepair.Services.Data.Organizations;
    using SelectAndRepair.Services.Data.OrganizationsServices;
    using SelectAndRepair.Services.Data.Services;
    using SelectAndRepair.Services.Mapping;
    using SelectAndRepair.Services.Messaging;
    using SelectAndRepair.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IVotesRepository, VotesRepository>();
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddSingleton<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ICitiesService, CitiesService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IOrganizationsService, OrganizationsService>();
            services.AddTransient<IOrganizationsServicesService, OrganizationsServicesService>();
            services.AddTransient<IServicesService, ServicesService>();
            services.AddTransient<IBlogPostsService, BlogPostsService>();

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = this.configuration["AuthenticationFacebook:AppId"];
                options.AppSecret = this.configuration["AuthenticationFacebook:AppSecret"];
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = this.configuration["AuthenticationGoogle:ClientId"];
                options.ClientSecret = this.configuration["AuthenticationGoogle:ClientSecret"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
