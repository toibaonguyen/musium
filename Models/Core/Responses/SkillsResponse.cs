
using System.Text.Json;
using JobNet.DTOs;

namespace JobNet.Models.Core.Responses;

public class SkillsResponse : BaseResponse
{
    public required IList<SkillDTO> Skills { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}