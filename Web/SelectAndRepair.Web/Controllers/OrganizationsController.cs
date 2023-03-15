namespace SelectAndRepair.Web.Controllers
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Data.Repositories;
    using SelectAndRepair.Services.Data.Organizations;
    using SelectAndRepair.Services.Messaging;
    using SelectAndRepair.Web.ViewModels.Organizations;

    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationsService organizationsService;
        private readonly IServiceProvider serviceProvider;
        private readonly IRazorViewEngine razorViewEngine;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IEmailSender emailSender;
        private readonly IVotesRepository votes;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationsController(
            IOrganizationsService organizationsService,
            IServiceProvider serviceProvider,
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IEmailSender emailSender,
            IVotesRepository votes,
            UserManager<ApplicationUser> userManager)
        {
            this.organizationsService = organizationsService;
            this.serviceProvider = serviceProvider;
            this.razorViewEngine = razorViewEngine;
            this.tempDataProvider = tempDataProvider;
            this.emailSender = emailSender;
            this.votes = votes;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new OrganizationsByUserListViewModel
            {
                Organizations = await this.organizationsService.GetAllAsync<BaseOrganizationViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.organizationsService.GetByIdAsync<OrganizationWithServicesViewModel>(id);
            viewModel.Rating = await votes.GetOrganizationRating(id);
            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult SendToEmail(string id, object test = null)
        {
            return this.RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendToEmail(string id)
        {
            var organization = this.organizationsService.GetByIdAsync<OrganizationWithServicesViewModel>(id);
            //var htmlData = await this.RenderToStringAsync("~/Views/Organizations/Details.cshtml", organization.Result);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{organization.Result.Name}</h1>");
            html.AppendLine($"<h3>{organization.Result.CategoryName}</h3>");
            html.AppendLine($"<img src=\"{organization.Result.ImageUrl}\" />");
            html.AppendLine($"<h5>{organization.Result.CityName}</h5>");
            html.AppendLine($"<p>{organization.Result.Address}</p>");
            foreach (var service in organization.Result.OrganizationService)
            {
                html.AppendLine($"<div><p class\"font-weight-bolder pb-2\">{service.ServiceName}</p><p>{service.ServiceDescription}</p></div>");
            }

            await this.emailSender.SendEmailAsync("0011-sid@unibit.bg", "SelectAndRepair", this.User.Identity.Name, organization.Result.Name, html.ToString());

            return this.RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Vote(string organizationId, int star)
        {
            if (star == 0)
            {
                return this.RedirectToAction("Details", new { id = organizationId });
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.votes.AddVote(organizationId, user, star);
            return this.RedirectToAction("Details", new { id = organizationId });
        }

        private async Task<string> RenderToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = this.serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = this.razorViewEngine.GetView(null, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary =
                    new ViewDataDictionary(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    { Model = model };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, this.tempDataProvider),
                    sw,
                    new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
