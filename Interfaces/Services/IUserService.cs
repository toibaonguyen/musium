


using JobNet.DTOs;
using JobNet.Models.Entities;
namespace JobNet.Interfaces.Services;
public interface IUserService
{
    Task CreateNewInactiveUser(CreateUserDTO user);
    Task CreateNewActiveUser(CreateUserDTO user);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(int id);
    Task<ProfileUserDTO?> GetProfileUserById(int id);
    Task<IEnumerable<ListUserDTO>> GetListOfUser();
    Task ChangeUserAvatar(int userId, string newAvatar);
    Task ChangeUserBackground(int userId, string newBackground);
    Task ChangeUserName(int userId, string name);
    Task ChangeSkills(int userId, IList<string> skills);
    Task ChangeBirthday(int userId, DateTime birthday);
    Task ChangeLocation(int userId, string location);
    Task ChangeActiveStatus(int userId, bool isActive);
    Task ChangeEmailConfirmationStatus(int userId, bool isEmailConfirmed);
}