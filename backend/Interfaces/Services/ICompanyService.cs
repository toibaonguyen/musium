using JobNet.DTOs;
using JobNet.Enums;

namespace JobNet.Interfaces.Services;

public interface ICompanyService
{
    Task CreateCompany(CreateCompanyDTO company);
    Task<CompanyDTO> UpdateCompany(int companyId, CreateCompanyDTO company);
    Task UpdateCompanyStatus(int companyId, bool IsActive);

}