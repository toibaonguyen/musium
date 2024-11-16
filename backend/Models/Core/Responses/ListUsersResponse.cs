
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class ListUsersResponse : BaseResponse
{
    public required IList<ListUserDTO> Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}