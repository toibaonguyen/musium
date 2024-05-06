
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserAvatarRequest : BaseRequest
{
    [Required]
    public required IFormFile Avatar { get; set; }
}
