using MinimalWebApi.Models;

namespace MinimalWebApi.Services.Contracts
{
    public interface IArticleService
    {
        Task<Article?> GetAsync(int id);
        Task<IList<Article>> GetAllAsync();
        Task<Article?> UpdateAsync(Article article);
        Task<Article> CreateAsync(Article article);
        Task DeleteAsync(int id);

    }
}
