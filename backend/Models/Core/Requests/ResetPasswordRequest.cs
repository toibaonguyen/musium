


using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class ResetPasswordRequest : BaseRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}