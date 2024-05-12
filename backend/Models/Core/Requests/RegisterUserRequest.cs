

using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;
namespace JobNet.Models.Core.Requests;

public class RegisterUserRequest : BaseRequest
{
    [Required]
    public required CreateUserDTO Data { get; set; }
}
