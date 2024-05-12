using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class ConversationBoxResponse : BaseResponse
{
    public required ConversationDTO Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}