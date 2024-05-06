using System.ComponentModel.DataAnnotations;

namespace JobNet.Models.Core.Requests;


public class UpdateConnectionStatusRequest : BaseRequest
{
    [Required]
    public required string Status { get; set; }
}