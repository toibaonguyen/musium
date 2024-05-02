
namespace JobNet.Models.Entities;
public class CloudMessageRegistrationToken : Entity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public required string Token { get; set; }
    public DateTime Timestamp { get; set; }
}