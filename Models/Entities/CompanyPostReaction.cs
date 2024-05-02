using JobNet.Enums;

namespace JobNet.Models.Entities;

public class CompanyPostReaction : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int CompanyPostId { get; set; }
    public CompanyPost CompanyPost { get; set; } = null!;
    public PostReactionType React { get; set; }
}