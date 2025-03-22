using System.ComponentModel.DataAnnotations;

namespace Notes.Models;

public class DataForm
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Description { get; set; }
}