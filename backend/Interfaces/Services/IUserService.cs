


using JobNet.DTOs;
using JobNet.Models.Entities;
namespace JobNet.Interfaces.Services;
public interface IUserService
{
    Task CreateNewInactiveUser(CreateUserDTO user, bool isEmailConfirmed);
    Task CreateNewActiveUser(CreateUserDTO user, bool isEmailConfirmed);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(int id);
    Task<ProfileUserDTO?> GetActiveProfileUserDTOById(int id);
    Task<IList<ListUserDTO>> GetActiveListUserDTOs(int limit, DateTime cursor, string? keyword);
    Task<IList<ListUserDTO>> GetListUserDTOs(int limit, DateTime cursor, string? keyword);
    Task ChangeUserPassword(int userId, string newPassword);
    Task<ProfileUserDTO> ChangeUserAvatar(int userId, IFormFile newAvatar);
    Task<ProfileUserDTO> ChangeUserBackground(int userId, IFormFile newBackground);
    Task<ProfileUserDTO> ChangeUserName(int userId, string name);
    Task<ProfileUserDTO> ChangeBirthday(int userId, DateTime birthday);
    Task<ProfileUserDTO> ChangeExperiences(int userId, IList<CreateExperienceDTO> Experiences);
    Task<ProfileUserDTO> ChangeCertifications(int userId, IList<CreateCertificationDTO> certifications);
    Task<ProfileUserDTO> ChangeEducations(int userId, IList<CreateEducationDTO> certifications);
    Task<ProfileUserDTO> ChangeSkills(int userId, IList<CreateSkillDTO> skillDTOs);
    Task<ProfileUserDTO> ChangeLocation(int userId, string location);
    Task ChangeActiveStatus(int userId, bool isActive);
    Task ChangeEmailConfirmationStatus(int userId, bool isEmailConfirmed);
    Task DeleteUser(int userId);
    Task<IEnumerable<ConnectionDTO>> GetConnectionDTOsOfUserByUserId(int userId, int limit, int offset);
    Task<IEnumerable<ConnectionRequestDTO>> GetConnectionRequestDTOsOfUserByUserId(int userId, int limit, DateTime cursor);
    Task<IEnumerable<ListFollowingCompanyDTO>> GetListFollowingCompanyDTOsOfUserByUserId(int userId);
    Task<IEnumerable<PostDTO>> GetActivePostDTOsOfUserByUserId(int userId, int limit, int offset);
    Task<IEnumerable<NotificationDTO>> GetNotificationDTOsOfUserByUserId(int userId, int limit, DateTime cursor);
}