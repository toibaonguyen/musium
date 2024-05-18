using System.IdentityModel.Tokens.Jwt;

namespace JobNet.Models.Core.Common;

public class EmailMessageBrokerModel : BaseCommonModel
{
    public required string Type { get; set; }
    public required string ToEmail { get; set; }
    public required string Content { get; set; }
}