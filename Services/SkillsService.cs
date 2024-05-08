

using JobNet.DTOs;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;

namespace JobNet.Services;

public class SkillsService : ISkillService
{
    public Task CreateNewSkill(string skillName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSkill(int skillId)
    {
        throw new NotImplementedException();
    }

    public Task<Skill?> GetSkillById(int skillId)
    {
        throw new NotImplementedException();
    }

    public Task<Skill?> GetSkillByName(string skillName)
    {
        throw new NotImplementedException();
    }

    public Task<IList<SkillDTO>> GetSkillDTOs(int limit)
    {
        throw new NotImplementedException();
    }

    public Task<IList<SkillDTO>> GetSkillDTOs(string similar, int limit)
    {
        throw new NotImplementedException();
    }
}