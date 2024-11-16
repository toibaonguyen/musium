namespace JobNet.Models.Entities;

public class UserSkill : Entity
{
    public int UserId { get; set; }
    public int SkillId { get; set; }
    public User User { get; set; } = null!;
    public Skill Skill { get; set; } = null!;
}