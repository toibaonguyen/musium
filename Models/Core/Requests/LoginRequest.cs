

using JobNet.DTOs;
using JobNet.Models.Core.Requests;

public class LoginRequest : BaseRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}