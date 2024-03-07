using System;
using MongoDB.Bson;
namespace JobNet.Models;
public class User
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public string? BackgroundImage { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Location { get; set; }
    public DateOnly Birthday { get; set; }
    public required ObjectId[] Certifications { get; set; }
    public required ObjectId[] Educations { get; set; }
    public required string[] Skills { get; set; }
    public bool IsActive { get; set; }
}