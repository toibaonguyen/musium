
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class CheckCloudMessageRegistrationTokenIsStoredRequest : BaseRequest
{
    [Required]
    public required CloudMessageRegistrationTokenDTO Data { get; set; }
}
