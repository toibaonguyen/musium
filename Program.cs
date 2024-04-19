using Microsoft.EntityFrameworkCore;
using JobNet.Settings;
using JobNet.Services;
// using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using JobNet.Data;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using JobNet.Middlewares;
using JobNet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JobNet.Contants;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

JobNetDatabaseSettings dbsettings = builder.Configuration.GetSection("JobNetDatabase").Get<JobNetDatabaseSettings>() ?? throw new Exception("missing setting in app.json file");
JWTAuthSettings tokenAuthSettings = builder.Configuration.GetSection("JWTAuth").Get<JWTAuthSettings>() ?? throw new Exception("missing setting in app.json file");

string redisConnectionString = builder.Configuration.GetSection("RedisConnectionString").Value ?? "";
if (String.IsNullOrEmpty(redisConnectionString))
{
    throw new Exception("Redis connection string is missing!");
}
string connectionString = dbsettings.ConnectionString;
if (String.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string is missing!");
}

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

builder.Services.Configure<JobNetDatabaseSettings>(builder.Configuration.GetSection("JobNetDatabase"));
builder.Services.Configure<EmailSenderProviderSetting>(builder.Configuration.GetSection("EmailSettingProvider"));
builder.Services.Configure<JWTAuthSettings>(builder.Configuration.GetSection("JWTAuth"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = tokenAuthSettings.ValidIssuerURL,
            ValidAudience = tokenAuthSettings.ValidAudienceURL,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenAuthSettings.SecretKey))

        };
    }
);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminPolicyName, p => p.RequireClaim(ClaimTypes.Role, UserRoles.Admin));
    options.AddPolicy(IdentityData.UserPolicyName, p => p.RequireClaim(ClaimTypes.Role, UserRoles.User));
});


builder.Services.AddDbContext<JobNetDatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UsersService>();
builder.Services.AddTransient<IAdminService, AdminsService>();

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
