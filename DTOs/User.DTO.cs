using JobNet.Models.Entities;
using MongoDB.Bson;

namespace JobNet.DTOs;

public class UserDTO
{
    public ObjectId Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string BackgroundImage { get; set; }
    public required string Email { get; set; }
    public required string Location { get; set; }
    public DateTime Birthday { get; set; }
    public required IEnumerable<ObjectId> Certifications { get; set; }
    public required IEnumerable<ObjectId> Educations { get; set; }
    public required IEnumerable<string> Skills { get; set; }
    public bool? IsHiring { get; set; }
}

