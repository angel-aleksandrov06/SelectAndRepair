namespace SelectAndRepair.Services.Data.BlogPosts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogPostsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);

        Task AddAsync(string title, string content, string author, string imageUrl);

        Task DeleteAsync(int id);
    }
}
