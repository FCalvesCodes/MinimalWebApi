using Microsoft.EntityFrameworkCore;
using MinimalWebApi.Data;
using MinimalWebApi.Models;
using MinimalWebApi.Services.Contracts;

namespace MinimalWebApi.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApiContext context;

        public ArticleService(ApiContext context)
        {
            this.context = context;
        }

        public async Task<Article> CreateAsync(Article article)
        {
            var articleCreated = context.Articles.Add(article).Entity;
            await context.SaveChangesAsync();
            return articleCreated;
        }

        public async Task DeleteAsync(int id)
        {
            var article = await GetAsync(id);

            if (article == null)
            {
                throw new Exception("Erro ao excluir o registro.");
            }

            context.Articles.Remove(article);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Article>> GetAllAsync()
        {
            return await context.Articles.ToListAsync();
        }

        public async Task<Article?> GetAsync(int id)
        {
            return await context.Articles.FindAsync(id);
        }

        public async Task<Article?> UpdateAsync(Article article)
        {
            var articleToUpdate = await GetAsync(article.Id);

            if (articleToUpdate == null)
            {
                return null;
            }

            articleToUpdate.Title = article.Title;
            articleToUpdate.Content = article.Content;
            articleToUpdate.PublishedAt = article.PublishedAt;

            context.Update(articleToUpdate);

            await context.SaveChangesAsync();

            return articleToUpdate;
        }
    }
}
