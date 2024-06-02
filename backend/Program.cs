using Microsoft.EntityFrameworkCore;
using JobNet.Settings;
using JobNet.Services;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using JobNet.Data;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using JobNet.Middlewares;
using JobNet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JobNet.Contants;
using System.Security.Claims;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using JobNet.Hubs;
using JobNet.DTOs;
using Microsoft.AspNetCore.SignalR;
using JobNet.Interfaces.Hubs;
using FirebaseAdmin.Messaging;
using JobNet.Services.Background;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.


//set environment variable GOOGLE_APPLICATION_CREDENTIALS by service-account-file.json before running below code
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault(),
    ProjectId = builder.Configuration["FirebaseSetting:ProjectId"] ?? throw new Exception("missing firebase's projectId in app.json file")
});
JobNetDatabaseSettings dbsettings = builder.Configuration.GetSection("JobNetDatabase").Get<JobNetDatabaseSettings>() ?? throw new Exception("missing setting in app.json file");
JWTAuthSettings tokenAuthSettings = builder.Configuration.GetSection("JWTAuth").Get<JWTAuthSettings>() ?? throw new Exception("missing setting in app.json file");

string redisConnectionString = builder.Configuration["RedisConnectionString"] ?? throw new Exception("Missing RedisConnectionString");

//set environment variable AZURE_STORAGE_CONNECTION_STRING by connection string of azure blob storage before running below code
string azureConnectionString = builder.Configuration["Azure:AZURE_STORAGE_CONNECTION_STRING"] ?? throw new Exception("Missing azure connection string");//Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING") ?? throw new Exception("Missing azure connection string!");

if (string.IsNullOrEmpty(redisConnectionString))
{
    throw new Exception("Redis connection string is missing!");
}
string connectionString = dbsettings.ConnectionString;
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string is missing!");
}

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

builder.Services.Configure<AzureSetting>(options => { options.ConnectionString = azureConnectionString; });
builder.Services.Configure<JobNetDatabaseSettings>(builder.Configuration.GetSection("JobNetDatabase"));
builder.Services.Configure<EmailSenderProviderSetting>(builder.Configuration.GetSection("EmailSettingProvider"));
builder.Services.Configure<JWTAuthSettings>(builder.Configuration.GetSection("JWTAuth"));
builder.Services.Configure<NotificationSetting>(builder.Configuration.GetSection("NotificationSetting"));
builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMqConfiguration"));


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

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(IdentityData.AdminPolicyName, p => p.RequireClaim(ClaimTypes.Role, UserRoles.Admin))
    .AddPolicy(IdentityData.UserPolicyName, p => p.RequireClaim(ClaimTypes.Role, UserRoles.User));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
                      builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
});

builder.Services.AddDbContext<JobNetDatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UsersService>();
builder.Services.AddTransient<IAdminService, AdminsService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ISkillService, SkillsService>();
builder.Services.AddTransient<ICloudMessageRegistrationTokenService, CloudMessageRegistrationTokenService>();
builder.Services.AddScoped<ICloudMessageTokenHandlerService, CloudMessageRegistrationTokenService>();
builder.Services.AddSingleton<IFirebaseCloudNotificationService, FirebaseCloudNotificationService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<IPostReactService, PostReactService>();
builder.Services.AddScoped<IPrivateChatService, PrivateChatService>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
//builder.Services.AddSingleton<INotificationService, NotificationService>();

builder.Services.AddHostedService<BackgroundNotificationSenderService>();
builder.Services.AddHostedService<BackgroundScaleTokensRemoverService>();
builder.Services.AddHostedService<BackgroundEmailSenderService>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

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

//============================================Test service api============================================//
app.MapPost("Test/send-message-to-user/{userId}", async (string userId, MessageDTO message, IHubContext<PrivateChatHub, IPrivateChatListenerHub> context) =>
{
    await context.Clients.User(userId).RecieveMessage(message);
    return Results.NoContent();
});
app.MapPost("Test/fcm/{clientToken}", async (string clientToken) =>
{
    Message message = new()
    {
        Data = new Dictionary<string, string>()
        {
            { "score", "850" },
            { "time", "2:45" },
        },
        Notification = new Notification()
        {

            Title = "$GOOG up 1.43% on the day",
            Body = "$GOOG gained 11.80 points to close at 835.67, up 1.43% on the day."
        },

        Token = clientToken,
    };
    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
    Console.WriteLine($"Phan hoi khi gui thong bao!! {response}");
    return Results.NoContent();
});
//=======================================End test service api=========================================//

app.MapHub<PrivateChatHub>("chathub");

app.Run();
