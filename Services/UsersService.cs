using JobNet.Data;
using Microsoft.EntityFrameworkCore;
using JobNet.DTOs;
using JobNet.Models.Entities;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Exceptions;
using JobNet.Utilities;


namespace JobNet.Services;

public class UsersService : IUserService
{
    private readonly string USER_EMAIL_IS_ALREADY_REGISTERED = "This user is already registered";
    private readonly string USER_IS_NOT_EXIST = "This user is not exist";
    private readonly string SKILL_IS_EXIST_BUT_CAN_NOT_GET = "Some thing wrong when take skill from database";
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly ISkillService _skillService;
    public UsersService(JobNetDatabaseContext databaseContext, ISkillService skillService)
    {
        _databaseContext = databaseContext;
        _skillService = skillService;
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
    public async Task<ProfileUserDTO?> GetProfileUserDTOById(int id)
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
    public async Task<IEnumerable<ListUserDTO>> GetListUserDTOs()
    {
        try
        {
            var users = await _databaseContext.Users.ToListAsync();
            var listUsers = new List<ListUserDTO>();
            foreach (User user in users)
            {
                listUsers.Add(user.ToListUserDTO());
            }
            return listUsers ?? ([]);
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
    public async Task ChangeUserBackground(int userId, string newBackground)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.BackgroundImage = newBackground;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserName(int userId, string name)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.Name = name;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeSkills(int userId, IList<string> skills)
    {
        try
        {
            var user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            var userSkills = _databaseContext.UserSkills.Where(e => e.UserId == userId).ToList();
            _databaseContext.UserSkills.RemoveRange(userSkills);
            IList<UserSkill> newUserSkills = [];
            foreach (var skill in skills)
            {
                var savedSkill = await _skillService.GetSkillByName(skill);
                if (savedSkill == null)
                {
                    await _skillService.CreateNewSkill(skill);
                    UserSkill userSkill = new()
                    {
                        UserId = userId,
                        SkillId = (await _skillService.GetSkillByName(skill) ?? throw new Exception(SKILL_IS_EXIST_BUT_CAN_NOT_GET)).Id
                    };
                    newUserSkills.Add(userSkill);
                }
            }
            await _databaseContext.UserSkills.AddRangeAsync(newUserSkills);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeBirthday(int userId, DateTime birthday)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.Birthday = birthday;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeLocation(int userId, string location)
    {
        try
        {
            User? user = await GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            user.Location = location;
            await _databaseContext.SaveChangesAsync();
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
}