using SelectAndRepair.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectAndRepair.Data.Repositories
{
    public interface IVotesRepository
    {
        Task<double> GetOrganizationRating(string organizationId);

        Task AddVote(string organizationId, ApplicationUser user, int value);

        Task<bool> CheckIfUserAlreayVoted(ApplicationUser user, string organizationId);
    }
}
