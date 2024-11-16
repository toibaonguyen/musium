
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class AdminResponse : BaseResponse
{
    public required AdminDTO Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}