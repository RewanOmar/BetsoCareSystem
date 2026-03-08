using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _repo;

        public ArticlesController(IArticleRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles(int page = 1, int pageSize = 5)
        {
            var articles = await _repo.GetAllAsync(page, pageSize);
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _repo.GetByIdAsync(id);

            if (article == null)
                return NotFound();

            return Ok(article);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var result = await _repo.SearchAsync(keyword);
            return Ok(result);
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> WeeklyArticles()
        {
            var articles = await _repo.GetWeeklyArticlesAsync();
            return Ok(articles);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var articles = await _repo.GetByCategoryAsync(category);
            return Ok(articles);
        }
    }
}