using Microsoft.EntityFrameworkCore;
using JobNet.Settings;
using JobNet.Services;
// using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using JobNet.Data;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using JobNet.Middlewares;
using JobNet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

JobNetDatabaseSettings dbsettings = builder.Configuration.GetSection("JobNetDatabase").Get<JobNetDatabaseSettings>() ?? throw new Exception("Something wrongs with setting the database");
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

builder.Services.AddDbContext<JobNetDatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UsersService>();
builder.Services.AddScoped<IAdminService, AdminsService>();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddNewtonsoftJson();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
