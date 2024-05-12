using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class CreateUserDTO
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(15)]
    public required string Password { get; set; }
    [Required]
    public required string Location { get; set; }
    [Required]
    public required DateTime Birthday { get; set; }
}
