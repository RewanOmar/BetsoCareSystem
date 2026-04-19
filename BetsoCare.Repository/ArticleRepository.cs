using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetsoCare.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= USER APIs =================

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles
    .OrderByDescending(a => a.PublishDate)
    .ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Article>> SearchAsync(string keyword)
        {
            return await _context.Articles
                .Where(a => a.Title.Contains(keyword) || a.Content.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetWeeklyArticlesAsync()
        {
            if (!await _context.Articles.AnyAsync())
                return new List<Article>();

            var latestDate = await _context.Articles.MaxAsync(a => a.PublishDate);
            var lastWeek = latestDate.AddDays(-7);

            return await _context.Articles
                .Where(a => a.PublishDate >= lastWeek)
                .OrderByDescending(a => a.PublishDate)
                .ToListAsync();
        }

        // ================= DASHBOARD APIs =================

        public async Task<Article> AddArticleAsync(Article article)
        {
            article.CreatedAt = DateTime.UtcNow;

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return article;
        }

        public async Task<Article?> UpdateArticleAsync(Article article)
        {
            var existing = await _context.Articles.FindAsync(article.Id);

            if (existing == null)
                return null;

            existing.Title = article.Title;
            existing.Summary = article.Summary;
            existing.Content = article.Content;
            existing.ImageUrl = article.ImageUrl;
            existing.Source = article.Source;
            existing.PublishDate = article.PublishDate;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
                return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Article>> GetByCategoryAsync(string category)
        {
            return await _context.Articles
                .Where(a => a.Category.ToLower() == category.ToLower())
                .OrderByDescending(a => a.PublishDate)
                .ToListAsync();
        }

        
    }
}