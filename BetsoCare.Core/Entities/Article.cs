using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Article
    {
        public int Id { get; set; }

       
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }

       
        public string? TitleEn { get; set; }
        public string? SummaryEn { get; set; }
        public string? ContentEn { get; set; }

        public string? ImageUrl { get; set; }
        public string? Source { get; set; }
        public string Category { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
