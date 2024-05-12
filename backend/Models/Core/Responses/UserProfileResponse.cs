
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class UserProfileResponse : BaseResponse
{
    public required bool IsConnected { get; set; }
    public required ProfileUserDTO? Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}