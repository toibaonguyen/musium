using System;
namespace JobNet.Models.Entities;
public class Certification : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string IssuingOrganizationName { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialID { get; set; }
    public string? CredentialURL { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
