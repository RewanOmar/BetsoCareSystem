using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/dashboard/articles")]
    //[Authorize(Roles = "Admin")]
    public class DashboardArticlesController : ControllerBase
    {
        private readonly IArticleRepository _repo;
        private readonly ApplicationDbContext _context;
        private readonly AppNotificationService _notificationService;

        public DashboardArticlesController(
            IArticleRepository repo,
            ApplicationDbContext context,
            AppNotificationService notificationService)
        {
            _repo = repo;
            _context = context;
            _notificationService = notificationService;
        }

        // ================= ADD ARTICLE =================
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromForm] CreateArticleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? imagePath = null;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/articles");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imagePath = "/images/articles/" + fileName;
            }

            var article = new Article
            {
                Title = dto.Title,
                Summary = dto.Summary,
                Content = dto.Content,

                TitleEn = dto.TitleEn ?? dto.Title,
                SummaryEn = dto.SummaryEn ?? dto.Summary,
                ContentEn = dto.ContentEn ?? dto.Content,

                Source = dto.Source,
                Category = dto.Category,
                PublishDate = dto.PublishDate,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = imagePath
            };

            await _repo.AddArticleAsync(article);

            // 🔔 Notification لكل المستخدمين
            var userIds = _context.Users.Select(u => u.Id).ToList();

            await _notificationService.CreateForAll(
                userIds,
               "📢 New Article Available",
                $"A new article has been published: {article.Title}"
            );

            return Ok(article);
        }

        
        // ================= UPDATE ARTICLE =================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(
            int id,
            [FromForm] UpdateArticleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _repo.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            existing.Title = dto.Title ?? existing.Title;
            existing.Summary = dto.Summary ?? existing.Summary;
            existing.Content = dto.Content ?? existing.Content;

            existing.TitleEn = dto.TitleEn ?? existing.TitleEn;
            existing.SummaryEn = dto.SummaryEn ?? existing.SummaryEn;
            existing.ContentEn = dto.ContentEn ?? existing.ContentEn;

            existing.Source = dto.Source ?? existing.Source;
            existing.Category = dto.Category ?? existing.Category;
            existing.PublishDate = dto.PublishDate ?? existing.PublishDate;

            var result = await _repo.UpdateArticleAsync(
                existing,
                dto.Image
            );

            // 🔔 Notification
            var userIds = _context.Users.Select(u => u.Id).ToList();

            await _notificationService.CreateForAll(
                userIds,
                "Article Updated",
                $"The article \"{existing.Title}\" has been updated."
            );

            return Ok(result);
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var deleted = await _repo.DeleteArticleAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Article deleted");
        }

        // ================= GET ALL =================
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _repo.GetAllAsync();
            return Ok(articles);
        }
    }
}