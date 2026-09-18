using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _repo;

        public ArticlesController(IArticleRepository repo)
        {
            _repo = repo;
        }

        // ==============================
        // ✅ GET ALL
        // ==============================
        [HttpGet]
        public async Task<IActionResult> GetArticles(string lang = "ar")
        {
            var articles = await _repo.GetAllAsync();

            var result = articles.Select(a => new
            {
                a.Id,
                title = lang == "en" ? a.TitleEn ?? a.Title : a.Title,
                summary = lang == "en" ? a.SummaryEn ?? a.Summary : a.Summary,
                content = lang == "en" ? a.ContentEn ?? a.Content : a.Content,
                imageUrl = $"{Request.Scheme}://{Request.Host}{a.ImageUrl}",
                a.Category,
                a.Source,
                a.PublishDate
            });

            return Ok(result);
        }

        // ==============================
        // ✅ GET BY ID
        // ==============================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id, string lang = "ar")
        {
            var a = await _repo.GetByIdAsync(id);

            if (a == null)
                return NotFound();

            var result = new
            {
                a.Id,
                title = lang == "en" ? a.TitleEn ?? a.Title : a.Title,
                summary = lang == "en" ? a.SummaryEn ?? a.Summary : a.Summary,
                content = lang == "en" ? a.ContentEn ?? a.Content : a.Content,
                imageUrl = $"{Request.Scheme}://{Request.Host}{a.ImageUrl}",
                a.Category,
                a.Source,
                a.PublishDate
            };

            return Ok(result);
        }

        // ==============================
        // ✅ SEARCH
        // ==============================
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword, string lang = "ar")
        {
            var articles = await _repo.GetAllAsync();

            var filtered = articles.Where(a =>
                lang == "en"
                ? (a.TitleEn != null && a.TitleEn.Contains(keyword)) ||
                  (a.ContentEn != null && a.ContentEn.Contains(keyword))
                : a.Title.Contains(keyword) ||
                  a.Content.Contains(keyword)
            );

            var result = filtered.Select(a => new
            {
                a.Id,
                title = lang == "en" ? a.TitleEn ?? a.Title : a.Title,
                summary = lang == "en" ? a.SummaryEn ?? a.Summary : a.Summary,
                content = lang == "en" ? a.ContentEn ?? a.Content : a.Content,
                imageUrl = $"{Request.Scheme}://{Request.Host}{a.ImageUrl}",
                a.Category,
                a.Source,
                a.PublishDate
            });

            return Ok(result);
        }

        // ==============================
        // ✅ WEEKLY
        // ==============================
        [HttpGet("weekly")]
        public async Task<IActionResult> WeeklyArticles(string lang = "ar")
        {
            var articles = await _repo.GetWeeklyArticlesAsync();

            var result = articles.Select(a => new
            {
                a.Id,
                title = lang == "en" ? a.TitleEn ?? a.Title : a.Title,
                summary = lang == "en" ? a.SummaryEn ?? a.Summary : a.Summary,
                content = lang == "en" ? a.ContentEn ?? a.Content : a.Content,
                imageUrl = $"{Request.Scheme}://{Request.Host}{a.ImageUrl}",
                a.Category,
                a.Source,
                a.PublishDate
            });

            return Ok(result);
        }

        // ==============================
        // ✅ CATEGORY
        // ==============================
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category, string lang = "ar")
        {
            var articles = await _repo.GetByCategoryAsync(category);

            var result = articles.Select(a => new
            {
                a.Id,
                title = lang == "en" ? a.TitleEn ?? a.Title : a.Title,
                summary = lang == "en" ? a.SummaryEn ?? a.Summary : a.Summary,
                content = lang == "en" ? a.ContentEn ?? a.Content : a.Content,
                imageUrl = $"{Request.Scheme}://{Request.Host}{a.ImageUrl}",
                a.Category,
                a.Source,
                a.PublishDate
            });

            return Ok(result);
        }
    }
}