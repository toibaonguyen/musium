
using JobNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using JobNet.Settings;
using Microsoft.Extensions.Options;

namespace JobNet.Data;
public class JobNetDatabaseContext : DbContext
{
    private readonly JobNetDatabaseSettings _settings;
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<CloudMessageRegistrationToken> CloudMessageRegistrationTokens { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyPageAdmin> CompanyPageAdmins { get; set; }
    public DbSet<CompanyPost> CompanyPosts { get; set; }
    public DbSet<CompanyPostComment> CompanyPostComments { get; set; }
    public DbSet<CompanyPostReaction> CompanyPostReactions { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<JobPost> JobPosts { get; set; }
    public DbSet<JobPostBill> JobPostBills { get; set; }
    public DbSet<JobPostSkill> JobPostSkills { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ConnectionRequestNotification> ConnectionRequestNotifications { get; set; }
    public DbSet<PostNotification> PostNotifications { get; set; }
    public DbSet<CompanyPostNotification> CompanyPostNotifications { get; set; }
    public DbSet<MessageNotification> MessageNotifications { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostReaction> PostReactions { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserFollowCompany> UserFollowCompanies { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }


    public JobNetDatabaseContext(DbContextOptions options, IOptions<JobNetDatabaseSettings> settings)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        this._settings = settings.Value;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseIdentityByDefaultColumns();


        //UserSkill
        modelBuilder.Entity<UserSkill>().HasKey(e => new { e.UserId, e.SkillId });

        //UserFollowCompany
        modelBuilder.Entity<UserFollowCompany>().HasKey(e => new { e.UserId, e.CompanyId });
        modelBuilder.Entity<UserFollowCompany>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //User
        modelBuilder.Entity<User>().HasKey(e => e.Id);
        modelBuilder.Entity<User>().HasMany(e => e.Experiences).WithOne(e => e.Author).HasForeignKey(e => e.AuthorId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.Certifications).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.Educations).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.PageAdminAtCompanies).WithOne(e => e.PageAdmin).HasForeignKey(e => e.PageAdminId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.Posts).WithOne(e => e.Owner).HasForeignKey(e => e.OwnerId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.JobPosts).WithOne(e => e.Author).HasForeignKey(e => e.AuthorId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.SentMessages).WithOne(e => e.Sender).HasForeignKey(e => e.SenderId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.ReceivedMessages).WithOne(e => e.Receiver).HasForeignKey(e => e.ReceiverId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.PostComments).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.PostReactions).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.FollowCompanies).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.CompanyPostComments).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.CompanyPostReactions).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.CloudMessageRegistrationTokens).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.UserSkills).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.InviterConnections).WithOne(e => e.Sender).HasForeignKey(e => e.SenderId).IsRequired();
        modelBuilder.Entity<User>().HasMany(e => e.InviteeConnections).WithOne(e => e.Reciever).HasForeignKey(e => e.RecieverId).IsRequired();

        //Skill
        modelBuilder.Entity<Skill>().HasKey(e => e.Id);
        modelBuilder.Entity<Skill>().HasMany(e => e.UserSkills).WithOne(e => e.Skill).HasForeignKey(e => e.SkillId).IsRequired();
        modelBuilder.Entity<Skill>().HasMany(e => e.JobPostSkills).WithOne(e => e.Skill).HasForeignKey(e => e.SkillId).IsRequired();

        //PostReaction
        modelBuilder.Entity<PostReaction>().HasKey(e => new { e.UserId, e.PostId });
        modelBuilder.Entity<PostReaction>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //Post
        modelBuilder.Entity<Post>().HasKey(e => e.Id);
        modelBuilder.Entity<Post>().HasMany(e => e.Comments).WithOne(e => e.Post).HasForeignKey(e => e.PostId).IsRequired();
        modelBuilder.Entity<Post>().HasMany(e => e.Reactions).WithOne(e => e.Post).HasForeignKey(e => e.PostId).IsRequired();
        modelBuilder.Entity<Post>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<Post>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<Post>().HasOne(e => e.Notification).WithOne(e => e.Post).HasForeignKey<PostNotification>(e => e.PostId).IsRequired();

        //--------------------------Notifications---------------------//

        //PostNotification
        modelBuilder.Entity<PostNotification>().HasKey(e => e.Id);
        modelBuilder.Entity<PostNotification>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //MessageNotification
        modelBuilder.Entity<MessageNotification>().HasKey(e => e.Id);
        modelBuilder.Entity<MessageNotification>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //ConnectionRequestNotification
        modelBuilder.Entity<ConnectionRequestNotification>().HasKey(e => e.Id);
        modelBuilder.Entity<ConnectionRequestNotification>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //CompanyPostNotification
        modelBuilder.Entity<CompanyPostNotification>().HasKey(e => e.Id);
        modelBuilder.Entity<CompanyPostNotification>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //------------------------------------------------------------//

        //Message
        modelBuilder.Entity<Message>().HasKey(e => e.Id);

        //JobPostSkill
        modelBuilder.Entity<JobPostSkill>().HasKey(e => new { e.SkillId, e.JobPostId });

        //JobPostBill
        modelBuilder.Entity<JobPostBill>().HasKey(e => e.Id);

        //JobPost
        modelBuilder.Entity<JobPost>().HasKey(e => e.Id);
        modelBuilder.Entity<JobPost>().HasMany(e => e.JobPostSkills).WithOne(e => e.JobPost).HasForeignKey(e => e.JobPostId).IsRequired();
        modelBuilder.Entity<JobPost>().HasOne(e => e.Bill).WithOne(e => e.JobPost).HasForeignKey<JobPostBill>(e => e.JobPostId).IsRequired();

        //Experience
        modelBuilder.Entity<Experience>().HasKey(e => e.Id);

        //Education
        modelBuilder.Entity<Education>().HasKey(e => e.Id);

        //Connection
        modelBuilder.Entity<Connection>().HasKey(e => e.Id);
        modelBuilder.Entity<Connection>().HasIndex(e => new { e.SenderId, e.RecieverId }).IsUnique();
        modelBuilder.Entity<Connection>().HasOne(e => e.Notification).WithOne(e => e.Connection).HasForeignKey<ConnectionRequestNotification>(e => e.ConnectionRequestId).IsRequired();

        //CompanyPostReaction
        modelBuilder.Entity<CompanyPostReaction>().HasKey(e => new { e.UserId, e.CompanyPostId });

        //CompanyPostComment
        modelBuilder.Entity<CompanyPostComment>().HasKey(e => e.Id);
        modelBuilder.Entity<CompanyPostComment>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<CompanyPostComment>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //CompanyPost
        modelBuilder.Entity<CompanyPost>().HasKey(e => e.Id);
        modelBuilder.Entity<CompanyPost>().HasMany(e => e.Reactions).WithOne(e => e.CompanyPost).HasForeignKey(e => e.CompanyPostId).IsRequired();
        modelBuilder.Entity<CompanyPost>().HasMany(e => e.Comments).WithOne(e => e.Post).HasForeignKey(e => e.PostId).IsRequired();
        modelBuilder.Entity<CompanyPost>().HasOne(e => e.Notification).WithOne(e => e.CompanyPost).HasForeignKey<CompanyPostNotification>(e => e.CompanyPostId).IsRequired();

        //CompanyPageAdmin
        modelBuilder.Entity<CompanyPageAdmin>().HasKey(e => new { e.CompanyId, e.PageAdminId });

        //Company
        modelBuilder.Entity<Company>().HasMany(e => e.Posts).WithOne(e => e.OwnCompany).HasForeignKey(e => e.CompanyId).IsRequired();
        modelBuilder.Entity<Company>().HasMany(e => e.UserExperiences).WithOne(e => e.Company).HasForeignKey(e => e.CompanyId).IsRequired();
        modelBuilder.Entity<Company>().HasMany(e => e.PageAdmins).WithOne(e => e.Company).HasForeignKey(e => e.CompanyId).IsRequired();
        modelBuilder.Entity<Company>().HasMany(e => e.Followers).WithOne(e => e.Company).HasForeignKey(e => e.CompanyId).IsRequired();

        //Comment
        modelBuilder.Entity<Comment>().HasKey(e => e.Id);
        modelBuilder.Entity<Comment>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<Comment>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        //CloudMessageRegistrationToken
        modelBuilder.Entity<CloudMessageRegistrationToken>().HasKey(e => e.Id);

        //Certification
        modelBuilder.Entity<Certification>().HasKey(e => e.Id);

        //Admin
        modelBuilder.Entity<Admin>().HasKey(e => e.Id);
    }
}