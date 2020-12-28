namespace SelectAndRepair.Services.Data.OrganizationsServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrganizationsServicesService
    {
        Task AddAsync(string organizationId, IEnumerable<int> servicesIds);

        Task AddAsync(IEnumerable<string> organizationId, int serviceId);

        Task ChangeAvailableStatusAsync(string organizationId, int serviceId);
    }
}
