
using System.Text;
using System.Web;
using JobNet.Contants;
using JobNet.DTOs;
using JobNet.Enums;
using JobNet.Models.Entities;
using JobNet.Utilities;

namespace JobNet.Extensions;

public static class DataConverterExtensions
{
    public static User ToActiveUser(this CreateUserDTO dto, bool isEmailConfirmed)
    {
        string hashedPassword = PasswordUtil.HashPassword(dto.Password, out byte[] salt);
        User user = new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Avatar = "default",
            BackgroundImage = "default",
            Password = hashedPassword,
            PasswordSalt = salt,
            Location = dto.Location,
            Birthday = dto.Birthday,
            IsActive = true,
            IsEmailConfirmed = isEmailConfirmed
        };
        return user;
    }
    public static User ToInactiveUser(this CreateUserDTO dto, bool isEmailConfirmed)
    {
        string hashedPassword = PasswordUtil.HashPassword(dto.Password, out byte[] salt);
        User user = new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Avatar = "default",
            BackgroundImage = "default",
            Password = hashedPassword,
            PasswordSalt = salt,
            Location = dto.Location,
            Birthday = dto.Birthday,
            IsActive = false,
            IsEmailConfirmed = isEmailConfirmed
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
            Skills = user.UserSkills.Select(e => new SkillDTO { Id = e.SkillId, Name = e.Skill.Name }).ToList()
        };
        return dto;
    }
    public static ListUserDTO ToListUserDTO(this User user)
    {
        var experience = user.Experiences.OrderByDescending(e => e.StartDate).FirstOrDefault();
        StringBuilder currentJobPosition = new();
        if (experience != null)
        {
            if (experience.IsUserCurentlyWorking)
            {
                currentJobPosition.Append(experience.Title);
                currentJobPosition.Append(" at ");
                //Test here
                currentJobPosition.Append(experience.Company.Name);
            }
        }
        ListUserDTO dto = new()
        {
            Id = user.Id,
            Name = user.Name,
            Avatar = user.Avatar,
            Location = user.Location,
            CurrentJobPosition = currentJobPosition.ToString()
        };
        return dto;
    }
    public static Admin ToAdmin(this CreateAdminDTO dto)
    {
        string hashedPassword = PasswordUtil.HashPassword(dto.Password, out byte[] salt);
        Admin admin = new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = hashedPassword,
            PasswordSalt = salt,
            IsActive = dto.IsActive
        };
        return admin;
    }
    public static UserIdentityDTO ToUserIdentityDTO(this User user)
    {
        return new UserIdentityDTO
        {
            Id = user.Id,
            Email = user.Email,
            Role = UserRoles.User
        };
    }
    public static UserIdentityDTO ToUserIdentityDTO(this Admin admin)
    {
        return new UserIdentityDTO
        {
            Id = admin.Id,
            Email = admin.Email,
            Role = UserRoles.Admin
        };
    }

    public static PostDTO ToPostDTO(this Post post)
    {
        var reactionTypesInPost = post.Reactions.GroupBy(r => r.React).Select(r => new { ReactionType = r.Key, Count = r.Count() }).OrderByDescending(x => x.Count).ToList();
        List<string> Top3ReactionsInPost = [];
        for (int i = 0; i < 3; i++)
        {
            var reaction = reactionTypesInPost.Skip(i).FirstOrDefault();
            if (reaction is null)
            {
                break;
            }
            Top3ReactionsInPost.Add(reaction.ReactionType.ToString());
        }

        return new PostDTO
        {
            User = post.Owner.ToListUserDTO(),
            Id = post.Id,
            Content = post.Content,
            Images = post.Images,
            Videos = post.Videos,
            OtherFiles = post.OtherFiles,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            CommentCount = post.Comments.Count,
            ReactionCount = post.Reactions.Count,
            Top3Reactions = Top3ReactionsInPost,
        };
    }

    public static NotificationDTO ToNotificationDTO(this ConnectionRequestNotification notification)
    {
        return new NotificationDTO
        {
            Content = notification.Content,
            CreatedAt = notification.CreatedAt
        };
    }
    public static NotificationDTO ToNotificationDTO(this MessageNotification notification)
    {
        return new NotificationDTO
        {
            Content = notification.Content,
            CreatedAt = notification.CreatedAt
        };
    }
    public static NotificationDTO ToNotificationDTO(this PostNotification notification)
    {
        return new NotificationDTO
        {
            Content = notification.Content,
            CreatedAt = notification.CreatedAt
        };
    }
    public static SkillDTO ToSkillDTO(this Skill skill)
    {
        return new SkillDTO
        {
            Id = skill.Id,
            Name = skill.Name
        };
    }
}