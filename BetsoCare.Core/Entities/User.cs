using BetsoCare.Core.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }   

    public string? PasswordHash { get; set; } 

    public string Role { get; set; } = "User";

    public string? ImageUrl { get; set; }

    public bool EmailConfirmed { get; set; } = false;
    public string? EmailVerificationToken { get; set; }
    public bool IsGoogleAccount { get; set; } = false;
    
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public string ProfileImageUrl { get; set; }
}