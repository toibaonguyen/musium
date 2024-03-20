using System;
namespace JobNet.Models.Entities;
public class AdminAccount
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string IsActive { get; set; }
}