namespace JobNet.Models.Entities;

public class JobPostSkill : Entity
{
    public int JobPostId { get; set; }
    public int SkillId { get; set; }
    public JobPost JobPost { get; set; } = null!;
    public Skill Skill { get; set; } = null!;
}