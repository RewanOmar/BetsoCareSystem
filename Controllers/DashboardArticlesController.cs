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

        // Add Article
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromForm] CreateArticleWithImageDto dto)
        {
            string? imagePath = null;

            if (dto.Image != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/articles",
                    fileName
                );

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = "/images/articles/" + fileName;
            }

            var article = new Article
            {
                Title = dto.Title,
                Summary = dto.Summary,
                Content = dto.Content,
                Source = dto.Source,
                Category = dto.Category,
                PublishDate = dto.PublishDate,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = imagePath
            };

            await _repo.AddArticleAsync(article);

            return Ok(article);
        }

        // Update Article
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] Article article)
        {
            article.Id = id;

            var result = await _repo.UpdateArticleAsync(article);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // Delete Article
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var deleted = await _repo.DeleteArticleAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Article deleted");
        }

        // Get all articles for dashboard
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _repo.GetAllAsync(1, 100);
            return Ok(articles);
        }
    }
}