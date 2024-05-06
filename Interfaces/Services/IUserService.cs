


using JobNet.DTOs;
using JobNet.Models.Entities;
namespace JobNet.Interfaces.Services;
public interface IUserService
{
    Task CreateNewInactiveUser(CreateUserDTO user, bool isEmailConfirmed);
    Task CreateNewActiveUser(CreateUserDTO user, bool isEmailConfirmed);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(int id);
    Task<ProfileUserDTO?> GetProfileUserDTOById(int id);
    Task<IEnumerable<ListUserDTO>> GetListUserDTOs();
    Task ChangeUserPassword(int userId, string newPassword);
    Task ChangeUserAvatar(int userId, IFormFile newAvatar);
    Task ChangeUserBackground(int userId, IFormFile newBackground);
    Task ChangeUserName(int userId, string name);
    Task ChangeSkills(int userId, IList<string> skills);
    Task ChangeBirthday(int userId, DateTime birthday);
    Task ChangeExperiences(int userId, IList<CreateExperienceDTO> Experiences);
    Task ChangeCertifications(int userId, IList<CreateCertificationDTO> certifications);
    Task ChangeEducations(int userId, IList<CreateEducationDTO> certifications);
    Task ChangeSkills(int userId, IList<CreateSkillDTO> skillDTOs);
    Task ChangeLocation(int userId, string location);
    Task ChangeActiveStatus(int userId, bool isActive);
    Task ChangeEmailConfirmationStatus(int userId, bool isEmailConfirmed);
    Task DeleteUser(int userId);
}