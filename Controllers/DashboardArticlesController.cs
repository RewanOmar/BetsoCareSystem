using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/dashboard/articles")]
    public class DashboardArticlesController : ControllerBase
    {
        private readonly IArticleRepository _repo;

        public DashboardArticlesController(IArticleRepository repo)
        {
            _repo = repo;
        }

        // ✅ ADD ARTICLE
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
                // عربي
                Title = dto.Title,
                Summary = dto.Summary,
                Content = dto.Content,

                // ✅ إنجليزي (fallback لو مش موجود)
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

            return Ok(article);
        }

        // ✅ UPDATE ARTICLE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] UpdateArticleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _repo.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            // عربي
            existing.Title = dto.Title ?? existing.Title;
            existing.Summary = dto.Summary ?? existing.Summary;
            existing.Content = dto.Content ?? existing.Content;

            // إنجليزي
            existing.TitleEn = dto.TitleEn ?? existing.TitleEn;
            existing.SummaryEn = dto.SummaryEn ?? existing.SummaryEn;
            existing.ContentEn = dto.ContentEn ?? existing.ContentEn;

            existing.Source = dto.Source ?? existing.Source;
            existing.Category = dto.Category ?? existing.Category;
            existing.PublishDate = dto.PublishDate ?? existing.PublishDate;

            var result = await _repo.UpdateArticleAsync(existing);

            return Ok(result);
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var deleted = await _repo.DeleteArticleAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Article deleted");
        }

        // ✅ GET ALL (Dashboard)
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _repo.GetAllAsync();
            return Ok(articles);
        }
    }
}