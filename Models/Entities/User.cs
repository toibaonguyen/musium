using System;
using Microsoft.EntityFrameworkCore;
namespace JobNet.Models.Entities;
public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string BackgroundImage { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Location { get; set; }
    public DateTime Birthday { get; set; }
    public required ICollection<Experience> Experiences { get; set; }
    public required ICollection<Certification> Certifications { get; set; }
    public required ICollection<Education> Educations { get; set; }
    public required ICollection<Group> JobNetGroups { get; set; }
    public required ICollection<Post> Posts { get; set; }
    public required IList<string> Skills { get; set; }
    public bool? IsHiring { get; set; }
    public bool IsActive { get; set; }
}