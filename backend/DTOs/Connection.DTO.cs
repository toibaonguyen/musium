
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class ConnectionDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string CurrentJobPosition { get; set; }
    public required string Location { get; set; }
}