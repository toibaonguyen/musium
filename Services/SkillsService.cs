

using JobNet.Data;
using JobNet.DTOs;
using JobNet.Exceptions;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobNet.Services;

public class SkillsService : ISkillService
{
    private readonly string SKILL_IS_ALREADY_EXIST = "Skill is already exist!";
    private readonly string SKILL_IS_NOT_EXIST = "Skill is not exist!";
    private readonly JobNetDatabaseContext _databaseContext;
    public SkillsService(JobNetDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public async Task CreateNewSkill(string skillName)
    {
        try
        {
            bool isExist = await _databaseContext.Skills.AnyAsync(x => x.Name == skillName);
            if (isExist)
            {
                throw new BadRequestException(SKILL_IS_ALREADY_EXIST);
            }
            await _databaseContext.Skills.AddAsync(new Skill
            {
                Name = skillName
            });
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteSkill(int skillId)
    {
        try
        {
            Skill? existence = await _databaseContext.Skills.FindAsync(skillId);
            if (existence is null)
            {
                throw new BadRequestException(SKILL_IS_NOT_EXIST);
            }
            _databaseContext.Skills.Remove(existence);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Skill?> GetSkillById(int skillId)
    {
        try
        {
            return await _databaseContext.Skills.FindAsync(skillId);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Skill?> GetSkillByName(string skillName)
    {
        try
        {
            return await _databaseContext.Skills.FirstOrDefaultAsync(e => e.Name == skillName);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<SkillDTO>> GetSkillDTOs(int limit = -1)
    {
        try
        {
            if (limit == -1)
            {
                return await _databaseContext.Skills.Select(e => e.ToSkillDTO()).OrderBy(e => e.Name).ToListAsync();
            }
            return await _databaseContext.Skills.Take(limit).Select(e => e.ToSkillDTO()).OrderBy(e => e.Name).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<SkillDTO>> GetSkillDTOs(string similar, int limit = -1)
    {
        try
        {
            if (limit == -1)
            {
                return await _databaseContext.Skills.Where(e => e.Name.Contains(similar)).Select(e => e.ToSkillDTO()).OrderBy(e => e.Name).ToListAsync();
            }
            return await _databaseContext.Skills.Where(e => e.Name.Contains(similar)).Take(limit).Select(e => e.ToSkillDTO()).OrderBy(e => e.Name).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}