using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class CreateArticleWithImageDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Summary is required")]
    public string Summary { get; set; }

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; }

    
    public string? Source { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; }

    [Required(ErrorMessage = "PublishDate is required")]
    public DateTime PublishDate { get; set; }

    public IFormFile? Image { get; set; } // optional
}