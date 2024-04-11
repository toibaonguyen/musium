using System;
using Microsoft.EntityFrameworkCore;
namespace JobNet.Models.Entities;
public class User : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string BackgroundImage { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required string Location { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<Experience> Experiences { get; } = new List<Experience>();
    public ICollection<Certification> Certifications { get; } = new List<Certification>();
    public ICollection<Education> Educations { get; } = new List<Education>();
    public ICollection<Group> AdminAtJobNetGroups { get; } = new List<Group>();
    public ICollection<Group> JobNetGroups { get; } = new List<Group>();
    public ICollection<Post> Posts { get; } = new List<Post>();
    public ICollection<JobPost> JobPosts { get; } = new List<JobPost>();
    public ICollection<Message> SentMessages { get; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; } = new List<Message>();
    public ICollection<Comment> Comments { get; } = new List<Comment>();
    public IList<string> Skills { get; set; } = new List<string>();
    public bool? IsHiring { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsEmailConfirmed { get; set; } = false;
}