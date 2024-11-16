
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class ChatMessagesResponse : BaseResponse
{
    public required IList<MessageDTO> Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}