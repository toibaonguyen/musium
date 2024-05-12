namespace JobNet.Models.Entities;

public class UserFollowCompany : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}