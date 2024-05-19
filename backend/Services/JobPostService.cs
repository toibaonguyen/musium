using JobNet.Data;
using JobNet.DTOs;
using JobNet.Enums;
using JobNet.Exceptions;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;

namespace JobNet.Services;

public class JobPostService : IJobPostService
{
    private readonly string SKILL_IS_EXIST_BUT_CAN_NOT_GET = "Some thing wrong when take skill from database";
    private readonly string INVALID_POST = "Invalid post!";
    private readonly string INVALID_COMPANY = "Invalid company!";
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly ICompanyService _companyService;
    private readonly ISkillService _skillService;
    public JobPostService(JobNetDatabaseContext databaseContext, ICompanyService companyService, ISkillService skillService)
    {
        _databaseContext = databaseContext;
        _companyService = companyService;
        _skillService = skillService;
    }
    public async Task ChangeJobPostStatus(int id, bool isActive)
    {
        try
        {
            var post = await _databaseContext.JobPosts.FindAsync(id) ?? throw new BadRequestException(INVALID_POST);
            post.IsActive = isActive;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListJobPostDTO> CreateJobPostAndSendToticationToFollowers(int companyId, CreateJobPostDTO post)
    {
        try
        {
            var company = await _companyService.GetCompanyById(companyId) ?? throw new BadRequestException(INVALID_COMPANY);
            JobPost jobPost = new()
            {
                JobTitle = post.JobTitle,
                Company = company,
                WorkplaceTypes = post.WorkplaceTypes.ToList().Select(t => (LocationType)Enum.Parse(typeof(LocationType), t)).ToList(),
                JobTypes = post.JobTypes.ToList().Select(t => (EmploymentType)Enum.Parse(typeof(EmploymentType), t)).ToList(),
                JobDescription = post.JobDescription,
                JobRequirements = post.JobRequirements,
                ContactInfo = post.ContactInfo,
                JobLocation = post.JobLocation,
                ExpiredAt = post.ExpiredAt,
                IsActive = post.IsActive,
            };
            await _databaseContext.SaveChangesAsync();
            List<JobPostSkill> skills = [];
            foreach (string skill in post.Skills)
            {
                var createdSkill = await _skillService.GetSkillByName(skill);
                if (createdSkill is null)
                {
                    await _skillService.CreateNewSkill(skill);
                }
                JobPostSkill newSki = new()
                {
                    JobPost = jobPost,
                    Skill = await _skillService.GetSkillByName(skill) ?? throw new Exception(SKILL_IS_EXIST_BUT_CAN_NOT_GET)
                };
                skills.Add(newSki);
            }
            await _databaseContext.JobPostSkills.AddRangeAsync(skills);
            await _databaseContext.SaveChangesAsync();
            return jobPost.ToListJobPostDTO();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteJobPost(int id)
    {
        try
        {
            var post = await _databaseContext.JobPosts.FindAsync(id) ?? throw new BadRequestException(INVALID_POST);
            _databaseContext.JobPostSkills.RemoveRange(post.JobPostSkills);
            _databaseContext.JobPosts.Remove(post);
            await _databaseContext.SaveChangesAsync();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<ListJobPostDTO>> GetActiveAndValidListJobPostDTOs(int limit, DateTime cursor, string keyword, List<string> skills, List<LocationType> locationTypes, List<EmploymentType> employmentTypes)
    {
        try
        {
            return await _databaseContext.JobPosts.Where(p => p.UpdatedAt <= cursor && p.JobPostSkills.Any(s => skills.Count < 1 || skills.Contains(s.Skill.Name)) && p.JobTitle.Contains(keyword) && p.WorkplaceTypes.Any(t => locationTypes.Count < 1 || locationTypes.Contains(t)) && p.JobTypes.Any(t => employmentTypes.Count < 1 || employmentTypes.Contains(t))).OrderByDescending(p => p.UpdatedAt).Take(limit).Select(p => p.ToListJobPostDTO()).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<ListJobPostDTO>> GetActiveAndValidListJobPostDTOsOfCompany(int limit, DateTime cursor, int companyId)
    {
        try
        {
            return await _databaseContext.JobPosts.Where(p => p.UpdatedAt <= cursor && p.CompanyId == companyId).OrderByDescending(e => e.CreatedAt).Take(limit).Select(p => p.ToListJobPostDTO()).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JobPostDTO?> GetActiveJobPostDTOById(int id)
    {
        try
        {
            return (await _databaseContext.JobPosts.FirstOrDefaultAsync(p => p.Id == id && p.IsActive))?.ToJobPostDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListJobPostDTO?> GetActiveListJobPostDTOById(int id)
    {
        try
        {
            return (await _databaseContext.JobPosts.FirstOrDefaultAsync(p => p.Id == id && p.IsActive))?.ToListJobPostDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<ListJobPostDTO>> GetListJobPostDTOs(int limit, DateTime cursor)
    {
        try
        {
            return await _databaseContext.JobPosts.Where(p => p.UpdatedAt <= cursor).Take(limit).Select(p => p.ToListJobPostDTO()).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<ListJobPostDTO>> GetListJobPostDTOsOfCompany(int limit, DateTime cursor, int companyId)
    {
        try
        {
            return await _databaseContext.JobPosts.Where(p => p.UpdatedAt <= cursor && p.CompanyId == companyId).Take(limit).Select(p => p.ToListJobPostDTO()).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateJobPost(int jobPostId, CreateJobPostDTO update)
    {
        try
        {
            var post = await _databaseContext.JobPosts.FindAsync(jobPostId) ?? throw new BadRequestException(INVALID_POST);
            _databaseContext.JobPostSkills.RemoveRange(post.JobPostSkills);
            await _databaseContext.SaveChangesAsync();
            List<JobPostSkill> skills = [];
            foreach (string skill in update.Skills)
            {
                var createdSkill = await _skillService.GetSkillByName(skill);
                if (createdSkill is null)
                {
                    await _skillService.CreateNewSkill(skill);
                }
                JobPostSkill newSki = new()
                {
                    JobPost = post,
                    Skill = await _skillService.GetSkillByName(skill) ?? throw new Exception(SKILL_IS_EXIST_BUT_CAN_NOT_GET)
                };
                skills.Add(newSki);
            }
            await _databaseContext.JobPostSkills.AddRangeAsync(skills);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}