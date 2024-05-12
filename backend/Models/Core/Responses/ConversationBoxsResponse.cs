using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class ConversationBoxsResponse : BaseResponse
{
    public required IList<ConversationDTO> Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}