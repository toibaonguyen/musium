
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateActivationStatusRequest : BaseRequest
{
    [Required]
    public required bool IsActive { get; set; }
}