using Microsoft.Extensions.Options;
using JobNet.Models;
using JobNet.Settings;
using JobNet.Data;
using Microsoft.EntityFrameworkCore;
using JobNet.DTOs;
using JobNet.Extensions;
using JobNet.Models.Entities;
using StackExchange.Redis;


namespace JobNet.Services;

public class AuthService
{
    private readonly UsersService _usersService;
    private readonly FilesService _filesService;
    private readonly EmailService _emailService;
    private readonly IConnectionMultiplexer _redis;
    public AuthService(UsersService usersService, FilesService filesService, EmailService emailService, IConnectionMultiplexer redis)
    {
        this._usersService = usersService;
        this._filesService = filesService;
        this._emailService = emailService;
        this._redis = redis;
    }
    public async Task RegisterNewUserAndSendVerifyEmail(CreateUserDTO newUser)
    {
        User user = newUser.ToInactiveUser();


    }
}