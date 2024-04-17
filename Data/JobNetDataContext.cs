
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
    public DbSet<Admin> Admins { get; set; }



    public JobNetDatabaseContext(DbContextOptions options, IOptions<JobNetDatabaseSettings> settings)
        : base(options)
    {
        Console.WriteLine($"Setting nay co connection string la:{settings.Value.ConnectionString}");
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        this._settings = settings.Value;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseIdentityByDefaultColumns();


        //User
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.Experiences).WithOne(e => e.Author).HasForeignKey(e => e.AuthorId).IsRequired();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.Certifications).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.Educations).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.AdminAtJobNetGroups).WithOne(e => e.Admin).HasForeignKey(e => e.AdminId).IsRequired();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.JobNetGroups).WithMany(e => e.Users);
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.JobNetGroups).WithMany(e => e.Users);
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.Posts).WithOne(e => e.Owner).HasForeignKey(e => e.OwnerId).IsRequired();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.JobPosts).WithOne(e => e.Author).HasForeignKey(e => e.AuthorId).IsRequired();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.SentMessages).WithOne(e => e.Sender).HasForeignKey(e => e.SenderId).IsRequired();
        modelBuilder.Entity<User>().ToTable(_settings.UsersTableName).HasMany(e => e.ReceivedMessages).WithOne(e => e.Receiver).HasForeignKey(e => e.ReceiverId).IsRequired();

        //Post
        modelBuilder.Entity<Post>().ToTable(_settings.PostsTableName).HasMany(e => e.Comments).WithOne(e => e.Post).HasForeignKey(e => e.PostId).IsRequired();

        //Message
        modelBuilder.Entity<Message>().ToTable(_settings.MessagesTableName);

        //JobPost
        modelBuilder.Entity<JobPost>().ToTable(_settings.JobPostsTableName);

        //Industry
        modelBuilder.Entity<Industry>().ToTable(_settings.IndustriesTableName).HasMany(e => e.Companies).WithOne(e => e.Industry).HasForeignKey(e => e.IndustryId).IsRequired();
        modelBuilder.Entity<Industry>().ToTable(_settings.IndustriesTableName).HasMany(e => e.Groups).WithOne(e => e.Industry).HasForeignKey(e => e.IndustryId).IsRequired();

        //Group
        modelBuilder.Entity<Group>().ToTable(_settings.GroupsTableName).HasMany(e => e.Posts).WithOne(e => e.Group).HasForeignKey(e => e.GroupId);

        //Experience
        modelBuilder.Entity<Experience>().ToTable(_settings.ExperiencesTableName);

        //Education
        modelBuilder.Entity<Education>().ToTable(_settings.EducationsTableName);

        //CompanyPostComment
        modelBuilder.Entity<CompanyPostComment>().ToTable(_settings.CompanyPostCommentsTableName);

        //CompanyPost
        modelBuilder.Entity<CompanyPost>().ToTable(_settings.CompanyPostsTableName).HasMany(e => e.Comments).WithOne(e => e.Post).HasForeignKey(e => e.PostId).IsRequired();

        //Company
        modelBuilder.Entity<Company>().ToTable(_settings.CompaniesTableName).HasMany(e => e.Posts).WithOne(e => e.OwnCompany).HasForeignKey(e => e.CompanyId).IsRequired();
        modelBuilder.Entity<Company>().ToTable(_settings.CompaniesTableName).HasMany(e => e.Experiences).WithOne(e => e.Company).HasForeignKey(e => e.CompanyId).IsRequired();

        //Comment
        modelBuilder.Entity<Comment>().ToTable(_settings.CommentsTableName);

        //Certification
        modelBuilder.Entity<Certification>().ToTable(_settings.CertificationsTableName);

        //AdminAccount
        modelBuilder.Entity<Admin>().ToTable(_settings.AdminsTableName);
    }
}