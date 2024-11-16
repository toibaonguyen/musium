
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class CompanyResponse : BaseResponse
{
    public required bool IsFollowing { get; set; }
    public required CompanyDTO? Data { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}