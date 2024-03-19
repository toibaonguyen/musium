namespace JobNet.Settings;
public class JobNetDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;

    public string UsersCollectionName { get; set; } = string.Empty;
    public string PostsCollectionName { get; set; } = string.Empty;
    public string MessagesCollectionName { get; set; } = string.Empty;
    public string JobPostsCollectionName { get; set; } = string.Empty;
    public string IndustriesCollectionName { get; set; } = string.Empty;
    public string GroupsCollectionName { get; set; } = string.Empty;
    public string ExperiencesCollectionName { get; set; } = string.Empty;
    public string EducationsCollectionName { get; set; } = string.Empty;
    public string CompaniesCollectionName { get; set; } = string.Empty;
    public string CommentsCollectionName { get; set; } = string.Empty;
    public string CertificationsCollectionName { get; set; } = string.Empty;
    public string AdminAccountsCollectionName { get; set; } = string.Empty;
}