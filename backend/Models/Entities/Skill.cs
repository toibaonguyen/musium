namespace JobNet.Models.Entities;

public class Skill : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<JobPostSkill> JobPostSkills { get; } = [];
    public ICollection<UserSkill> UserSkills { get; } = [];
}