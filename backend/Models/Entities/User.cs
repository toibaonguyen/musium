
namespace JobNet.Models.Entities;
public class User : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string BackgroundImage { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required string Location { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<Experience> Experiences { get; } = [];
    public ICollection<Certification> Certifications { get; } = [];
    public ICollection<Education> Educations { get; } = [];
    public ICollection<Connection> InviteeConnections { get; } = [];
    public ICollection<Connection> InviterConnections { get; } = [];
    public ICollection<Post> Posts { get; } = [];
    public ICollection<JobPost> JobPosts { get; } = [];
    public ICollection<Message> SentMessages { get; } = [];
    public ICollection<UserInConversation> Conversations { get; } = [];
    public ICollection<Comment> PostComments { get; } = [];
    public ICollection<PostReaction> PostReactions { get; } = [];
    public ICollection<UserFollowCompany> FollowCompanies { get; } = [];
    public ICollection<CloudMessageRegistrationToken> CloudMessageRegistrationTokens { get; } = [];
    public ICollection<ConnectionRequestNotification> ConnectionRequestNotifications { get; } = [];
    public ICollection<PostNotification> PostNotifications { get; } = [];
    public ICollection<JobPostNotification> JobPostNotifications { get; } = [];
    public ICollection<MessageNotification> MessageNotifications { get; } = [];
    public ICollection<UserSkill> UserSkills { get; } = [];
    public required bool IsActive { get; set; }
    public required bool IsEmailConfirmed { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}