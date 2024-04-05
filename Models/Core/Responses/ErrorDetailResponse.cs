
using System.Text.Json;

namespace JobNet.Models.Core.Responses;

public class ErrorDetailsResponse : BaseResponse
{
    public required string Type { get; set; }
    public required string Title { get; set; }
    public int Status { get; set; }
    public required string Details { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}