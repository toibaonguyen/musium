
namespace JobNet.DTOs;
/*Admin DTO*/
public class AdminDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required bool IsActive { get; set; }
}