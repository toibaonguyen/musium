
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class JobPostResponse : BaseResponse
{
    public required JobPostDTO? Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}