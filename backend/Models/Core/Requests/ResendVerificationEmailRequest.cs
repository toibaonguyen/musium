


using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class ResendVerificationEmailRequest : BaseRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}