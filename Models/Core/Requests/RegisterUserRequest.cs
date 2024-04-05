

using JobNet.DTOs;
namespace JobNet.Models.Core.Requests;

public class RegisterUserRequest : BaseRequest
{
    public required CreateUserDTO Data { get; set; }
}
