
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class CommentsResponse : BaseResponse
{
    public required IList<CommentDTO> Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}