using System;
using MongoDB.Bson;
namespace JobNet.Models;
public class Certification
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string Name { get; set; }
    public required string IssuingOrganizationName { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public string? CredentialID { get; set; }
    public string? CredentialURL { get; set; }
}