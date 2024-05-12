using System;
namespace JobNet.Models.Entities;
public class Admin : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required bool IsActive { get; set; }
}