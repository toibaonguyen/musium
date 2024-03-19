
using JobNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.EntityFrameworkCore.Extensions;
using JobNet.Settings;
using Microsoft.Extensions.Options;

namespace JobNet.Data;
public class JobNetDatabaseContext : DbContext
{
    private readonly JobNetDatabaseSettings _settings;
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<JobPost> JobPosts { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Certification> Certifications { get; set; }



    public JobNetDatabaseContext(DbContextOptions options, IOptions<JobNetDatabaseSettings> settings)
        : base(options)
    {
        Console.WriteLine($"Setting nay co connection string la:{settings.Value.ConnectionString}");
        this._settings = settings.Value;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine($"May del lam viec ak 0 {modelBuilder}");
        base.OnModelCreating(modelBuilder);
        Console.WriteLine($"May del lam viec ak {modelBuilder}");

        modelBuilder.Entity<User>().ToCollection(_settings.UsersCollectionName);
        modelBuilder.Entity<Post>().ToCollection(_settings.PostsCollectionName);
        modelBuilder.Entity<Message>().ToCollection(_settings.MessagesCollectionName);
        modelBuilder.Entity<JobPost>().ToCollection(_settings.JobPostsCollectionName);
        modelBuilder.Entity<Industry>().ToCollection(_settings.IndustriesCollectionName);
        modelBuilder.Entity<Group>().ToCollection(_settings.GroupsCollectionName);
        modelBuilder.Entity<Experience>().ToCollection(_settings.ExperiencesCollectionName);
        modelBuilder.Entity<Education>().ToCollection(_settings.EducationsCollectionName);
        modelBuilder.Entity<Company>().ToCollection(_settings.CompaniesCollectionName);
        modelBuilder.Entity<Comment>().ToCollection(_settings.CommentsCollectionName);
        modelBuilder.Entity<Certification>().ToCollection(_settings.CertificationsCollectionName);
    }
}