

using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;
using JobNet.Models.Core.Requests;

public class LoginRequest : BaseRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}