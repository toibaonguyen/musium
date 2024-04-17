namespace JobNet.Settings;

public class EmailSenderProviderSetting
{
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ApiToken { get; set; } = string.Empty;
    public string ApiBaseUrl { get; set; } = string.Empty;
}