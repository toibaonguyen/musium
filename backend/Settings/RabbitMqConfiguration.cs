namespace JobNet.Settings;

public class RabbitMqConfiguration
{
    public required string HostName { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}