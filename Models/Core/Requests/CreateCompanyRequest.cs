

using System.ComponentModel.DataAnnotations;

namespace JobNet.Models.Core.Requests;

public class CreateCompanyRequest : BaseRequest
{
    [Required]
    public required CreateCompanyDTO Data { get; set; }
}