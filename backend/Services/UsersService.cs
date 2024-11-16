using JobNet.Data;
using Microsoft.EntityFrameworkCore;
using JobNet.DTOs;
using JobNet.Models.Entities;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Exceptions;
using JobNet.Utilities;
using JobNet.Enums;
using System.Text;

namespace JobNet.Services;

public class UsersService : IUserService
{
    private readonly string USER_EMAIL_IS_ALREADY_REGISTERED = "This user is already registered";
    private readonly string USER_IS_NOT_EXIST = "This user is not exist";
    private readonly string SKILL_IS_EXIST_BUT_CAN_NOT_GET = "Some thing wrong when take skill from database";
    private readonly string COMPANY_IS_NOT_EXIST = "This company is not exist";
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly ISkillService _skillService;
    private readonly IFileService _fileService;
    public UsersService(JobNetDatabaseContext databaseContext, ISkillService skillService, IFileService fileService)
    {
        _databaseContext = databaseContext;
        _skillService = skillService;
        _fileService = fileService;
    }
    public async Task CreateNewInactiveUser(CreateUserDTO user, bool isEmailConfirmed)
    {
        try
        {
            var existence = await _databaseContext.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
            if (existence != null)
            {
                //Throw exception because this user has been existed
                throw new BadRequestException(USER_EMAIL_IS_ALREADY_REGISTERED);
            }
            await _databaseContext.Users.AddAsync(user.ToInactiveUser(isEmailConfirmed));
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserPassword(int userId, string newPassword)
    {
        try
        {
            var existence = await GetUserById(userId) ?? throw new BadRequestException("User is not exist");
            string hashedPassword = PasswordUtil.HashPassword(newPassword, out byte[] salt);
            existence.Password = hashedPassword;
            existence.PasswordSalt = salt;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task CreateNewActiveUser(CreateUserDTO user, bool isEmailConfirmed)
    {
        try
        {
            var existence = await GetUserByEmail(user.Email);
            if (existence != null)
            {
                //Throw exception because this user has been existed
                throw new BadRequestException(USER_EMAIL_IS_ALREADY_REGISTERED);
            }
            await _databaseContext.Users.AddAsync(user.ToActiveUser(isEmailConfirmed));
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            var existence = await _databaseContext.Users.FirstOrDefaultAsync(e => e.Email == email);
            return existence;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<User?> GetUserById(int id)
    {
        try
        {
            var existence = await _databaseContext.Users.FindAsync(id);
            return existence;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ProfileUserDTO?> GetActiveProfileUserDTOById(int id)
    {
        try
        {
            var existence = await _databaseContext.Users.FindAsync(id);
            return existence?.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<IList<ListUserDTO>> GetListUserDTOs(int limit, DateTime cursor, string? keyword)
    {
        try
        {
            string pattern = keyword ?? "";
            return await _databaseContext.Users.Where(u => u.Name.Contains(pattern) && u.CreatedAt <= cursor).OrderByDescending(u => u.CreatedAt).Select(u => u.ToListUserDTO()).Take(limit).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<IList<ListUserDTO>> GetActiveListUserDTOs(int limit, DateTime cursor, string? keyword)
    {
        try
        {
            string pattern = keyword ?? "";
            return await _databaseContext.Users.Where(u => u.Name.Contains(pattern) && u.CreatedAt <= cursor && u.IsActive).OrderByDescending(u => u.CreatedAt).Select(u => u.ToListUserDTO()).Take(limit).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserAvatar(int userId, string newAvatar)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.Avatar = newAvatar;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ProfileUserDTO> ChangeUserBackground(int userId, string newBackground)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.BackgroundImage = newBackground;
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ProfileUserDTO> ChangeUserName(int userId, string name)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.Name = name;
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ProfileUserDTO> ChangeBirthday(int userId, DateTime birthday)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.Birthday = birthday;
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ProfileUserDTO> ChangeLocation(int userId, string location)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.Location = location;
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeActiveStatus(int userId, bool isActive)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.IsActive = isActive;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeEmailConfirmationStatus(int userId, bool isEmailConfirmed)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.IsEmailConfirmed = isEmailConfirmed;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task DeleteUser(int userId)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            _databaseContext.Users.Remove(user);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProfileUserDTO> ChangeUserAvatar(int userId, IFormFile newAvatar)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            await _fileService.DeleteFileAsync(user.Avatar);
            user.Avatar = (await _fileService.UploadFileAsync(newAvatar, $"{user.Id}-{Guid.NewGuid()}-{new DateTime()}")).Uri;
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProfileUserDTO> ChangeUserBackground(int userId, IFormFile newBackground)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            await _fileService.DeleteFileAsync(user.BackgroundImage);
            user.Avatar = (await _fileService.UploadFileAsync(newBackground, $"{user.Id}-{Guid.NewGuid()}-{new DateTime()}")).Uri;
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProfileUserDTO> ChangeExperiences(int userId, IList<CreateExperienceDTO> Experiences)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            var oldExs = user.Experiences;
            _databaseContext.Experiences.RemoveRange(oldExs);
            List<Experience> newExps = [];
            foreach (var experience in Experiences)
            {
                EmploymentType employmentType;
                Enum.TryParse(experience.EmploymentType, out employmentType);
                LocationType locationTypeType;
                Enum.TryParse(experience.LocationType, out locationTypeType);

                var newExp = new Experience
                {
                    Author = user,
                    Title = experience.Title,
                    EmploymentType = employmentType,
                    Location = experience.Location,
                    LocationType = locationTypeType,
                    Description = experience.Description,
                    StartDate = experience.StartDate,
                    EndDate = experience.EndDate,
                    Company = await _databaseContext.Companies.FindAsync(experience.CompanyId) ?? throw new BadRequestException(COMPANY_IS_NOT_EXIST)
                };
                newExps.Add(newExp);
            }
            await _databaseContext.Experiences.AddRangeAsync(newExps);
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProfileUserDTO> ChangeCertifications(int userId, IList<CreateCertificationDTO> certifications)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            var oldCers = user.Certifications;
            _databaseContext.Certifications.RemoveRange(oldCers);
            List<Certification> newCers = [];
            foreach (var cer in certifications)
            {

                var newCer = new Certification
                {
                    Name = cer.Name,
                    IssuingOrganizationName = cer.IssuingOrganizationName,
                    IssueDate = cer.IssueDate,
                    ExpirationDate = cer.ExpirationDate,
                    CredentialID = cer.CredentialID,
                    CredentialURL = cer.CredentialURL,
                    User = user
                };
                newCers.Add(newCer);
            }
            await _databaseContext.Certifications.AddRangeAsync(newCers);
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProfileUserDTO> ChangeEducations(int userId, IList<CreateEducationDTO> educations)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            var oldCers = user.Certifications;
            _databaseContext.Certifications.RemoveRange(oldCers);
            List<Education> newEdus = [];
            foreach (var education in educations)
            {

                var newEdu = new Education
                {
                    SchoolName = education.SchoolName,
                    Degree = education.Degree,
                    Major = education.Major,
                    StartDate = education.StartDate,
                    EndDate = education.EndDate,
                    Grade = education.Grade,
                    ActivitiesAndSocieties = education.ActivitiesAndSocieties,
                    Description = education.Description,
                    User = user
                };
                newEdus.Add(newEdu);
            }
            await _databaseContext.Educations.AddRangeAsync(newEdus);
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProfileUserDTO> ChangeSkills(int userId, IList<CreateSkillDTO> skills)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            var oldSkills = user.UserSkills;
            _databaseContext.UserSkills.RemoveRange(oldSkills);
            List<UserSkill> newSkills = [];
            foreach (var skill in skills)
            {
                var createdSkill = await _skillService.GetSkillByName(skill.Name);
                if (createdSkill is null)
                {
                    await _skillService.CreateNewSkill(skill.Name);
                }
                var newSki = new UserSkill
                {
                    User = user,
                    Skill = await _skillService.GetSkillByName(skill.Name) ?? throw new Exception(SKILL_IS_EXIST_BUT_CAN_NOT_GET)
                };
                newSkills.Add(newSki);
            }
            await _databaseContext.UserSkills.AddRangeAsync(newSkills);
            await _databaseContext.SaveChangesAsync();
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ConnectionDTO>> GetConnectionDTOsOfUserByUserId(int userId, int limit, int offset)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            List<ConnectionDTO> connections = [];
            List<Connection> inviteeCconnections = user.InviteeConnections.OrderByDescending(e => e.UpdatedAt).DistinctBy(e => e.SenderId).Where(e => e.Status == ConnectionRequestStatusType.ACCEPT).ToList();
            List<Connection> inviterCconnections = user.InviterConnections.OrderByDescending(e => e.UpdatedAt).DistinctBy(e => e.RecieverId).Where(e => e.Status == ConnectionRequestStatusType.ACCEPT).ToList();
            foreach (var connection in inviteeCconnections)
            {
                var experience = connection.Sender.Experiences.OrderByDescending(e => e.StartDate).FirstOrDefault();
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
                connections.Add(new ConnectionDTO
                {
                    Id = connection.SenderId,
                    Name = connection.Sender.Name,
                    Avatar = connection.Sender.Avatar,
                    CurrentJobPosition = currentJobPosition.ToString(),
                    Location = connection.Sender.Location
                });
            }
            foreach (var connection in inviterCconnections)
            {
                var experience = connection.Reciever.Experiences.OrderByDescending(e => e.StartDate).FirstOrDefault();
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
                connections.Add(new ConnectionDTO
                {
                    Id = connection.SenderId,
                    Name = connection.Sender.Name,
                    Avatar = connection.Sender.Avatar,
                    CurrentJobPosition = currentJobPosition.ToString(),
                    Location = connection.Sender.Location
                });
            }
            return connections.Skip(offset).Take(limit);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ConnectionRequestDTO>> GetConnectionRequestDTOsOfUserByUserId(int userId, int limit, DateTime cursor)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            List<ConnectionRequestDTO> requests = [];
            List<Connection> inviteeConnections = user.InviteeConnections.OrderByDescending(e => e.UpdatedAt).DistinctBy(e => e.SenderId).Where(e => e.Status == ConnectionRequestStatusType.PENDING && e.UpdatedAt <= cursor).Take(limit).ToList();
            foreach (var connection in inviteeConnections)
            {
                var experience = connection.Sender.Experiences.OrderByDescending(e => e.StartDate).FirstOrDefault();
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
                requests.Add(new ConnectionRequestDTO
                {
                    Id = connection.SenderId,
                    Name = connection.Sender.Name,
                    Avatar = connection.Sender.Avatar,
                    CurrentJobPosition = currentJobPosition.ToString(),
                    Location = connection.Sender.Location,
                    RequestedAt = connection.RequestedAt
                });
            }
            return requests.OrderByDescending(e => e.RequestedAt);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ListFollowingCompanyDTO>> GetListFollowingCompanyDTOsOfUserByUserId(int userId)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            return user.FollowCompanies.Select(c => new ListFollowingCompanyDTO
            {
                Id = c.CompanyId,
                Name = c.Company.Name,
                Avatar = c.Company.Avatar,
                NumberFollowers = c.Company.Followers.Count
            }).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<PostDTO>> GetActivePostDTOsOfUserByUserId(int userId, int limit, int offset)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            return user.Posts.Where(p => p.IsActive).Select(c => c.ToPostDTO()).Skip(offset).Take(limit).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<NotificationDTO>> GetNotificationDTOsOfUserByUserId(int userId, int limit, DateTime cursor)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            List<NotificationDTO> notifications = [];
            foreach (var notification in user.PostNotifications)
            {
                notifications.Add(notification.ToNotificationDTO());
            }
            foreach (var notification in user.ConnectionRequestNotifications)
            {
                notifications.Add(notification.ToNotificationDTO());
            }
            foreach (var notification in user.JobPostNotifications)
            {
                notifications.Add(notification.ToNotificationDTO());
            }
            return notifications.Where(e => e.CreatedAt <= cursor).OrderByDescending(e => e.CreatedAt).Take(limit);

        }
        catch (Exception)
        {
            throw;
        }
    }


}