using System.Reflection;
using System.Text;
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
        Console.WriteLine("post.Owner.Id:::::=>>>>>>>>>>>>>>>>>>>>");
        Console.WriteLine(post.Id);
        Console.WriteLine(post.OwnerId);

        return new PostDTO
        {
            User = post.Owner.ToListUserDTO(),
            Id = post.Id,
            Content = post.Content,
            Images = post.Images.Select(i => new FileDTO { Uri = i }).ToList(),
            Videos = post.Videos.Select(i => new FileDTO { Uri = i }).ToList(),
            OtherFiles = post.OtherFiles.Select(i => new FileDTO { Uri = i }).ToList(),
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
            NavigationType = ResourceNotificationType.POST.ToString(),
            Content = notification.Content,
            CreatedAt = notification.CreatedAt,
            ResourceId = notification.Connection.SenderId

        };
    }
    public static NotificationDTO ToNotificationDTO(this PostNotification notification)
    {
        return new NotificationDTO
        {
            NavigationType = ResourceNotificationType.CONNECTION.ToString(),
            Content = notification.Content,
            CreatedAt = notification.CreatedAt,
            ResourceId = notification.PostId

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
    public static AdminDTO ToAdminDTO(this Admin admin)
    {
        return new AdminDTO
        {
            Id = admin.Id,
            Name = admin.Name,
            Email = admin.Email,
            IsActive = admin.IsActive
        };
    }
    public static CommentDTO ToCommentDTO(this Comment comment)
    {
        return new CommentDTO
        {
            User = comment.User.ToChatUserDTO(),
            id = comment.Id,
            Content = comment.Content,
            Image = comment.Image
        };
    }
    public static CompanyDTO ToCompanyDTO(this Company company)
    {
        return new CompanyDTO
        {
            Id = company.Id,
            Name = company.Name,
            Avatar = company.Avatar,
            BackgroundImage = company.BackgroundImage,
            Description = company.Description,
            Website = company.Website,
            CompanySize = company.CompanySize,
            Headquarters = company.Headquarters,
            FoundedAt = company.FoundedAt,
            NumberOfFollowers = company.Followers.Count,
        };
    }
    public static MessageDTO ToMessageDTO(this Message message)
    {
        return new MessageDTO
        {
            SenderId = message.SenderId,
            ConversationId = message.ConversationId,
            Content = message.Content,
            ImageURL = message.Image,
            VideoURL = message.Video,
            OtherFileURL = message.OtherFile,
            SentAt = message.CreatedAt
        };
    }
    public static ChatUserDTO ToChatUserDTO(this User user)
    {
        return new ChatUserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Avatar = user.Avatar
        };
    }
    public static ConversationDTO ToConversationDTO(this Conversation conversation, ChatUserDTO withUser)
    {
        return new ConversationDTO
        {
            ConversationId = conversation.Id,
            LastestMessage = conversation.Messages.OrderByDescending(e => e.CreatedAt).First().ToMessageDTO(),
            WithUser = withUser
        };
    }
    public static ListJobPostDTO ToListJobPostDTO(this JobPost jobPost)
    {
        return new ListJobPostDTO()
        {
            Id = jobPost.Id,
            JobTitle = jobPost.JobTitle,
            CompanyName = jobPost.Company.Name,
            CompanyAvatar = jobPost.Company.Avatar,
            JobLocation = jobPost.JobLocation,
            WorkplaceType = jobPost.WorkplaceTypes.Select(e => e.ToString()).ToList()
        };
    }
    public static JobPostDTO ToJobPostDTO(this JobPost jobPost)
    {
        return new JobPostDTO()
        {
            Id = jobPost.Id,
            JobTitle = jobPost.JobTitle,
            JobLocation = jobPost.JobLocation,
            WorkplaceType = jobPost.WorkplaceTypes.Select(e => e.ToString()).ToList(),
            JobType = jobPost.JobTypes.Select(e => e.ToString()).ToList(),
            JobDescription = jobPost.JobDescription,
            Skills = jobPost.JobPostSkills.Select(s => s.Skill.Name).ToList(),
            JobRequirements = jobPost.JobRequirements,
            ContactInfo = jobPost.ContactInfo,
            ExpiredAt = jobPost.ExpiredAt
        };
    }
    public static ListCompanyDTO ToListCompanyDTO(this Company company)
    {
        return new ListCompanyDTO()
        {
            Id = company.Id,
            Name = company.Name,
            Avatar = company.Avatar,
            NumberOfFollowers = company.Followers.Count,
            Headquarters = company.Headquarters
        };
    }
    public static Dictionary<string, string> ToStringDictionary(this object obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        var dict = new Dictionary<string, string>();
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            var value = property.GetValue(obj, null);
            dict[property.Name] = value?.ToString() ?? string.Empty;
        }
        return dict;
    }
    public static NotificationDTO ToNotificationDTO(this MessageNotification notification)
    {
        return new NotificationDTO()
        {
            NavigationType = ResourceNotificationType.MESSAGE.ToString(),
            ResourceId = notification.Message.ConversationId,
            Content = notification.Content
        };
    }
    public static NotificationDTO ToNotificationDTO(this JobPostNotification notification)
    {
        return new NotificationDTO()
        {
            NavigationType = ResourceNotificationType.MESSAGE.ToString(),
            ResourceId = notification.PostId,
            Content = notification.Content
        };
    }
}