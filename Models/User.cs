namespace JobNet.Models;
public class User
{
    //may be i will change later
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly Birthday { get; set; }
    public bool IsActive { get; set; }
}