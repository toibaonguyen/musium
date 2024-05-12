
using System.Text.Json;

namespace JobNet.Models.Core.Responses;

public class MessageResponse : BaseResponse
{
    public required string Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}