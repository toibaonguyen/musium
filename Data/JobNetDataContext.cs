
using JobNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
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
    public DbSet<CompanyPost> CompanyPosts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<AdminAccount> AdminAccounts { get; set; }



    public JobNetDatabaseContext(DbContextOptions options, IOptions<JobNetDatabaseSettings> settings)
        : base(options)
    {
        Console.WriteLine($"Setting nay co connection string la:{settings.Value.ConnectionString}");
        this._settings = settings.Value;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseIdentityByDefaultColumns();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName);
        modelBuilder.Entity<Post>().ToTable(_settings.PostsTableName);
        modelBuilder.Entity<Message>().ToTable(_settings.MessagesTableName);
        modelBuilder.Entity<JobPost>().ToTable(_settings.JobPostsTableName);
        modelBuilder.Entity<Industry>().ToTable(_settings.IndustriesTableName);
        modelBuilder.Entity<Group>().ToTable(_settings.GroupsTableName);
        modelBuilder.Entity<Experience>().ToTable(_settings.ExperiencesTableName);
        modelBuilder.Entity<Education>().ToTable(_settings.EducationsTableName);
        modelBuilder.Entity<CompanyPost>().ToTable(_settings.CompanyPostsTableName);
        modelBuilder.Entity<Company>().ToTable(_settings.CompaniesTableName);
        modelBuilder.Entity<Comment>().ToTable(_settings.CommentsTableName);
        modelBuilder.Entity<Certification>().ToTable(_settings.CertificationsTableName);
        modelBuilder.Entity<AdminAccount>().ToTable(_settings.AdminAccountsTableName);
    }
}