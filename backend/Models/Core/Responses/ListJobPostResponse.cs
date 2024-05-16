
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class ListJobPostResponse : BaseResponse
{
    public required ListJobPostDTO? Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}