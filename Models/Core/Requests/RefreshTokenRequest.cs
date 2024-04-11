using System.ComponentModel.DataAnnotations;
using JobNet.Models.Core.Requests;

public class RefreshTokenRequest : BaseRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}