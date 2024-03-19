using System;
using MongoDB.Bson;
namespace JobNet.Models.Entities;
public class Company
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public string? BackgroundImage { get; set; }
    public required string Description { get; set; }
    public string? Website { get; set; }
    public required ObjectId Industry { get; set; }
    public required int CompanySize { get; set; }
    public required string Headquarters { get; set; }
    public required DateTime FoundedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime IsActive { get; set; }
}
