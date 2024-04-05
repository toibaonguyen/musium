using System;
namespace JobNet.Models.Entities;
public class Industry : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Company> Companies { get; } = new List<Company>();
    public ICollection<Group> Groups { get; } = new List<Group>();
}