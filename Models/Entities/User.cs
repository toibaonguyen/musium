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
    public ICollection<Experience> Experiences { get; } = new List<Experience>();
    public ICollection<Certification> Certifications { get; } = new List<Certification>();
    public ICollection<Education> Educations { get; } = new List<Education>();
    public ICollection<Group> JobNetGroups { get; } = new List<Group>();
    public ICollection<Post> Posts { get; } = new List<Post>();
    public ICollection<Message> Messages { get; } = new List<Message>();
    public required IList<string> Skills { get; set; }
    public bool? IsHiring { get; set; }
    public bool IsActive { get; set; }
}