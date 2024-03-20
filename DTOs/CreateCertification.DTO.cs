
namespace JobNet.DTOs;
public class CreateCertificationDTO
{
    //may be i will change later
    public required string Name { get; set; }
    public required string IssuingOrganizationName { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public string? CredentialID { get; set; }
    public string? CredentialURL { get; set; }
}