namespace Notes.Models;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Picture { get; set; }
    public bool IsEmailVerified { get; set; }
    public string Azp { get; set; }
    public string? Jti { get; set; }
    public string? Sub { get; set; }
}