using JobNet.DTOs;
using JobNet.Enums;
using JobNet.Models.Entities;

namespace JobNet.Interfaces.Services;

public interface ICompanyService
{
    Task CreateCompany(CreateCompanyDTO company);
    Task<CompanyDTO> UpdateCompany(int companyId, CreateCompanyDTO company);
    Task<bool> CheckIfUserIsFollowCompany(int userId, int companyId);
    Task UpdateCompanyStatus(int companyId, bool IsActive);
    Task<Company?> GetCompanyById(int companyId);
    Task<IList<ListCompanyDTO>> GetActiveListCompanyDTOs(int limit, int cursor, string keyword);
    Task<CompanyDTO?> GetActiveCompanyDTObyId(int id);
    Task<CompanyDTO?> GetCompanyDTOById(int id);
    Task FollowCompany(int userId, int companyId);
    Task UnFollowCompany(int userId, int companyId);

}