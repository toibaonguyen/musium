using Microsoft.EntityFrameworkCore;
using JobNet.Settings;
using MongoDB.Driver;
using JobNet.Services;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using JobNet.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
JobNetDatabaseSettings dbsettings = builder.Configuration.GetSection("JobNetDatabase").Get<JobNetDatabaseSettings>() ?? throw new Exception("Something wrongs with setting the database");
string connectionString = dbsettings.ConnectionString;
string databaseName = dbsettings.DatabaseName;
if (connectionString.IsNullOrEmpty())
{
    throw new Exception("Connection string is missing!");
}
builder.Services.Configure<JobNetDatabaseSettings>(builder.Configuration.GetSection("JobNetDatabase"));
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<PostsService>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<JobNetDatabaseContext>(options => options.UseMongoDB(connectionString, databaseName).GetType());

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
