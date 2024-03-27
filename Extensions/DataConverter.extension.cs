
using JobNet.DTOs;
using JobNet.Models.Entities;

namespace JobNet.Extensions;

public static class DataConverterExtensions
{
    public static User ToActiveUser(this CreateUserDTO dto, string avatar, string backgroundImage)
    {
        User user = new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Avatar = avatar,
            BackgroundImage = backgroundImage,
            Password = dto.Password,
            Location = dto.Location,
            Birthday = dto.Birthday,
            IsActive = true
        };
        return user;
    }
    public static User ToInactiveUser(this CreateUserDTO dto, string avatar, string backgroundImage)
    {
        User user = new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Avatar = avatar,
            BackgroundImage = backgroundImage,
            Password = dto.Password,
            Location = dto.Location,
            Birthday = dto.Birthday,
            IsActive = false,
        };
        return user;
    }

    public static CertificationDTO ToCertificationDTO(this Certification certification)
    {
        CertificationDTO dto = new()
        {
            Id = certification.Id,
            Name = certification.Name,
            IssuingOrganizationName = certification.IssuingOrganizationName,
            IssueDate = certification.IssueDate,
            ExpirationDate = certification.ExpirationDate,
            CredentialID = certification.CredentialID,
            CredentialURL = certification.CredentialURL
        };
        return dto;
    }
    public static EducationDTO ToEducationDTO(this Education education)
    {
        EducationDTO dto = new()
        {
            Id = education.Id,
            SchoolName = education.SchoolName,
            Degree = education.Degree,
            Major = education.Major,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            Grade = education.Grade,
            ActivitiesAndSocieties = education.ActivitiesAndSocieties,
            Description = education.Description
        };
        return dto;
    }
    public static ExperienceDTO ToExperienceDTO(this Experience experience)
    {
        string employmentType, locationType;
        switch (experience.EmploymentType)
        {
            case Enums.EmploymentType.FULLTIME:
                employmentType = "Full time";
                break;
            case Enums.EmploymentType.PARTTIME:
                employmentType = "Part time";
                break;
            case Enums.EmploymentType.SELF_EMPLOYED:
                employmentType = "Self employed";
                break;
            case Enums.EmploymentType.FREELANCE:
                employmentType = "Free lance";
                break;
            case Enums.EmploymentType.CONTRACT:
                employmentType = "Contract";
                break;
            case Enums.EmploymentType.INTERNSHIP:
                employmentType = "Internship";
                break;
            case Enums.EmploymentType.APPRENTICESHIP:
                employmentType = "Apprenticeship";
                break;
            case Enums.EmploymentType.SEASONAL:
                employmentType = "Seasonal";
                break;
            default:
                employmentType = "Full time";
                break;
        }
        switch (experience.LocationType)
        {
            case Enums.LocationType.ON_SITE:
                locationType = "On site";
                break;
            case Enums.LocationType.HYBRID:
                locationType = "Hybrid";
                break;
            case Enums.LocationType.REMOTE:
                locationType = "Remote";
                break;
            default:
                locationType = "On site";
                break;

        }
        ExperienceDTO dto = new()
        {
            Id = experience.Id,
            Title = experience.Title,
            EmploymentType = employmentType,
            Location = experience.Location,
            LocationType = locationType,
            Description = experience.Description,
            IsUserCurentlyWorking = experience.IsUserCurentlyWorking,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate
        };
        return dto;
    }
    public static ProfileUserDTO ToProfileUserDTO(this User user)
    {
        ProfileUserDTO dto = new()
        {
            Id = user.Id,
            Name = user.Name,
            Avatar = user.Avatar,
            BackgroundImage = user.BackgroundImage,
            Email = user.Email,
            Location = user.Location,
            Birthday = user.Birthday,
            Certifications = user.Certifications.Select(c => c.ToCertificationDTO()),
            Educations = user.Educations.Select(c => c.ToEducationDTO()),
            Experiences = user.Experiences.Select(e => e.ToExperienceDTO()),
            Skills = user.Skills,
            IsHiring = user.IsHiring,
        };
        return dto;
    }
}