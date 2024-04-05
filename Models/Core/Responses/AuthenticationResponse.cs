
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class AuthenticationResponse : BaseResponse
{
    public required AuthenticationTokenDTO Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}