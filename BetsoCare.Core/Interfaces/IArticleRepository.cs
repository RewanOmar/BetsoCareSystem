using BetsoCare.Core.Entities;

namespace BetsoCare.Core.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<IEnumerable<Article>> SearchAsync(string keyword);
        Task<IEnumerable<Article>> GetWeeklyArticlesAsync();
        Task<Article> AddArticleAsync(Article article);
        Task<Article?> UpdateArticleAsync(Article article);
        Task<bool> DeleteArticleAsync(int id);
        Task<IEnumerable<Article>> GetByCategoryAsync(string category);
        
    }
}