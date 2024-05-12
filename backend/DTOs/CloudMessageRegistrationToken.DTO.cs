
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;
public class CloudMessageRegistrationTokenDTO
{
    [Required]
    public required string Token { get; set; }
}