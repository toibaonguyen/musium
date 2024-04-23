
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class CreatedPostResponse : BaseResponse
{
    public required PostDTO Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}