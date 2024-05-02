using System;
namespace JobNet.Models.Entities;
public class CompanyPageAdmin : Entity
{
    public int CompanyId { get; set; }
    public int PageAdminId { get; set; }
    public Company Company { get; set; } = null!;
    public User PageAdmin { get; set; } = null!;
}
