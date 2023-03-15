using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SelectAndRepair.Data.Models;

namespace SelectAndRepair.Data.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private readonly ApplicationDbContext context;

        public VotesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<double> GetOrganizationRating(string organizationId)
        {

            var ratings = await this.context.Votes
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => x.Value)
                .ToListAsync();

            if (ratings.Any())
            {
                return ratings.Average();
            }

            return 0.0;
        }

        public async Task AddVote(string organizationId, ApplicationUser user, int value)
        {
            var vote = new Vote
            {
                User = user,
                OrganizationId = organizationId,
                Value = value
            };

            await context.Votes.AddAsync(vote);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfUserAlreayVoted(ApplicationUser user, string organizationId)
        {
            var voted = await this.context.Votes.AnyAsync(x => x.UserId == user.Id && x.OrganizationId == organizationId);

            return voted;
        }
    }
}
