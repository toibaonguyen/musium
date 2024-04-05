using System;
namespace JobNet.Models.Entities;
public class CompanyPost : Entity
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company OwnCompany { get; set; } = null!;
    public required string Content { get; set; }
    public required IList<string> Images { get; set; }
    public ICollection<CompanyPostComment> Comments { get; set; } = new List<CompanyPostComment>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }
}