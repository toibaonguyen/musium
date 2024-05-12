using System.ComponentModel.DataAnnotations;
using JobNet.Models.Core.Requests;

public class RefreshTokenRequest : BaseRequest
{
    [Required(ErrorMessage = "Refresh token is required")]
    public required string RefreshToken { get; set; }
}