namespace SelectAndRepair.Services.Data.BlogPosts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SelectAndRepair.Data.Common.Repositories;
    using SelectAndRepair.Data.Models;
    using SelectAndRepair.Services.Mapping;

    public class BlogPostsService : IBlogPostsService
    {
        private readonly IDeletableEntityRepository<BlogPost> blogPostsRepository;

        public BlogPostsService(IDeletableEntityRepository<BlogPost> blogPostsRepository)
        {
            this.blogPostsRepository = blogPostsRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.blogPostsRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();
        }

        public async Task AddAsync(string title, string content, string author, string imageUrl)
        {
            var blogPost = new BlogPost
            {
                Title = title,
                Content = content,
                Author = author,
                ImageUrl = imageUrl,
            };

            await this.blogPostsRepository.AddAsync(blogPost);
            await this.blogPostsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blogPost = await this.blogPostsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            this.blogPostsRepository.Delete(blogPost);
            await this.blogPostsRepository.SaveChangesAsync();
        }
    }
}
