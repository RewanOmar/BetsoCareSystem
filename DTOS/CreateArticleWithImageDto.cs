using Microsoft.AspNetCore.Http;

public class CreateArticleWithImageDto
{
    public string Title { get; set; }

    public string Summary { get; set; }

    public string Content { get; set; }

    public string Source { get; set; }

    public string Category { get; set; }

    public DateTime PublishDate { get; set; }

    public IFormFile? Image { get; set; } // optional
}