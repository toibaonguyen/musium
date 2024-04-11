
namespace JobNet.DTOs;

public class CreateAdminDTO
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required bool IsActive { get; set; }
}