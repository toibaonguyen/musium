
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class CreateSkillDTO
{
    [Required]
    public required string Name { get; set; }
}