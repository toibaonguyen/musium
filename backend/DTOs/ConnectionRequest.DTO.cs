
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class ConnectionRequestDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string CurrentJobPosition { get; set; }
    public required string Location { get; set; }
    public required DateTime RequestedAt { get; set; }
}