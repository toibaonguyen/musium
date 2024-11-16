
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class CreateAdminDTO
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
    public required bool IsActive { get; set; }
}