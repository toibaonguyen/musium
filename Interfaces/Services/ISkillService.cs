
using JobNet.DTOs;
using JobNet.Models.Entities;
namespace JobNet.Interfaces.Services;

public interface ISkillService
{
    Task CreateNewSkill(string skillName);
    Task DeleteSkill(int skillId);
    Task<Skill?> GetSkillById(int skillId);
    Task<Skill?> GetSkillByName(string skillName);
    Task<IList<SkillDTO>> GetSkillDTOs(int limit = -1);
    Task<IList<SkillDTO>> GetSkillDTOs(string similar, int limit = -1);
}