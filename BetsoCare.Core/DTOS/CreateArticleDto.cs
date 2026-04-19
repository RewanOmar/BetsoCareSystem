using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BetsoCare.Core.DTOS
{
    public class CreateArticleDto
    {
        // عربي
        [Required]
        public string Title { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        // 👇 أضيفي دول (إنجليزي optional)
        public string? TitleEn { get; set; }
        public string? SummaryEn { get; set; }
        public string? ContentEn { get; set; }

        public string? Source { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        // 👇 أضيفي الصورة
        public IFormFile? Image { get; set; }
    }
}