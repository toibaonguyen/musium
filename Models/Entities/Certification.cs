using System;
using MongoDB.Bson;
namespace JobNet.Models.Entities;
public class Certification
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string Name { get; set; }
    public required string IssuingOrganizationName { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialID { get; set; }
    public string? CredentialURL { get; set; }
}