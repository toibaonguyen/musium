
using System.Text.Json;

namespace JobNet.Models.Core.Responses;

public class ReactionTypesResponse : BaseResponse
{
    public required IList<string> Reactions { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}