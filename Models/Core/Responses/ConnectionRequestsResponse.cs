
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class ConnectionRequestsResponse : BaseResponse
{
    public required IList<ConnectionRequestDTO> Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}