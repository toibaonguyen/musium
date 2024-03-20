using System;
namespace JobNet.Models.Entities;
public class Group
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required User Admin { get; set; }
    public required string Description { get; set; }
    public required Industry Industry { get; set; }
    public required string Location { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required bool IsActive { get; set; }
}
