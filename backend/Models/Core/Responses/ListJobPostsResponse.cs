
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class ListJobPostsResponse : BaseResponse
{
    public required IList<ListJobPostDTO> Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}