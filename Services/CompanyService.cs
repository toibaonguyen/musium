
using JobNet.Data;
using JobNet.DTOs;
using JobNet.Exceptions;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
namespace JobNet.Services;

public class CompanyService : ICompanyService
{
    private readonly string INVALID_COMPANY = "Invalid company!";
    JobNetDatabaseContext _databaseContext;
    IFileService _fileService;
    ILogger<CompanyService> _logger;
    public CompanyService(JobNetDatabaseContext databaseContext, IFileService fileService, ILogger<CompanyService> logger)
    {
        _databaseContext = databaseContext;
        _fileService = fileService;
        _logger = logger;
    }
    public async Task CreateCompany(CreateCompanyDTO company)
    {
        try
        {
            Company newCompany = new Company
            {
                Name = company.Name,
                Avatar = (await _fileService.UploadFileAsync(company.Avatar, $"{company.Name}-{Guid.NewGuid()}-{new DateTime()}")).Uri,
                Description = company.Description,
                CompanySize = company.CompanySize,
                BackgroundImage = (await _fileService.UploadFileAsync(company.BackgroundImage, $"{company.Name}-{Guid.NewGuid()}-{new DateTime()}")).Uri,
                Headquarters = company.Headquarters,
                FoundedAt = company.FoundedAt
            };
            await _databaseContext.Companies.AddAsync(newCompany);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<CompanyDTO> UpdateCompany(int companyId, CreateCompanyDTO company)
    {
        try
        {
            Company? targetCompany = await _databaseContext.Companies.FindAsync(companyId) ?? throw new BadRequestException(INVALID_COMPANY);
            targetCompany.Name = company.Name;
            await _fileService.DeleteFileAsync(targetCompany.Avatar);
            targetCompany.Avatar = (await _fileService.UploadFileAsync(company.Avatar, $"{company.Name}-{Guid.NewGuid()}-{new DateTime()}")).Uri;
            targetCompany.Description = company.Description;
            targetCompany.CompanySize = company.CompanySize;
            await _fileService.DeleteFileAsync(targetCompany.BackgroundImage);
            targetCompany.BackgroundImage = (await _fileService.UploadFileAsync(company.Avatar, $"{company.Name}-{Guid.NewGuid()}-{new DateTime()}")).Uri;
            targetCompany.Headquarters = company.Headquarters;
            targetCompany.FoundedAt = company.FoundedAt;
            targetCompany.UpdatedAt = DateTime.UtcNow;
            await _databaseContext.SaveChangesAsync();

            return targetCompany.ToCompanyDTO();

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task UpdateCompanyStatus(int companyId, bool IsActive)
    {
        try
        {
            Company? targetCompany = await _databaseContext.Companies.FindAsync(companyId) ?? throw new BadRequestException(INVALID_COMPANY);
            targetCompany.IsActive = IsActive;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}