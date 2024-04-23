namespace JobNet.Models.Entities;

public class Skill : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<JobPost> JobPosts { get; } = [];
    public List<JobPostSkill> JobPostSkills { get; } = [];
    public List<User> Users { get; } = [];
    public List<UserSkill> UserSkills { get; } = [];
}