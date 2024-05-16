using JobNet.DTOs;
using JobNet.Enums;

namespace JobNet.Interfaces.Services;

public interface IJobPostService
{
    Task<ListJobPostDTO> CreateJobPost(int companyId, CreateJobPostDTO post);
    Task ChangeJobPostStatus(int id, bool isActive);
    Task DeleteJobPost(int id);
    Task UpdateJobPost(int jobPostId, CreateJobPostDTO update);
    Task<IList<ListJobPostDTO>> GetActiveAndValidListJobPostDTOs(int limit, DateTime cursor, string keyword, List<string> skills, List<LocationType> locationTypes, List<EmploymentType> employmentTypes);
    Task<IList<ListJobPostDTO>> GetActiveAndValidListJobPostDTOsOfCompany(int limit, DateTime cursor, int companyId);
    Task<IList<ListJobPostDTO>> GetListJobPostDTOsOfCompany(int limit, DateTime cursor, int companyId);
    Task<IList<ListJobPostDTO>> GetListJobPostDTOs(int limit, DateTime cursor);
    Task<ListJobPostDTO?> GetActiveListJobPostDTOById(int id);
    Task<JobPostDTO?> GetActiveJobPostDTOById(int id);
}