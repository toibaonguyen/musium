
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
            Experiences = [],
            Certifications = [],
            Educations = [],
            JobNetGroups = [],
            Skills = [],
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
            Experiences = [],
            Certifications = [],
            Educations = [],
            JobNetGroups = [],
            Skills = [],
            IsActive = false
        };
        return user;
    }
    // public static UserDTO ToUserDTO(this User user)
    // {
    //     UserDTO dto = new()
    //     {
    //         Id = user.Id,
    //         Name = user.Name,
    //         Avatar = user.Avatar,
    //         BackgroundImage = user.BackgroundImage,
    //         Email = user.Email,
    //         Location = user.Location,
    //         Birthday = user.Birthday,
    //         Certifications = user.Certifications,
    //         Educations = user.Educations,
    //         Skills = user.Skills,
    //         IsHiring = user.IsHiring,
    //     };
    //     return dto;
    // }
}