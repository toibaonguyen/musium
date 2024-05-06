
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class CreatePostReactionDTO
{
    [Required]
    public required string React { get; set; }
}