

namespace JobNet.DTOs;
public class CertificationDTO
{
    //may be i will change later
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string IssuingOrganizationName { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialID { get; set; }
    public string? CredentialURL { get; set; }
}