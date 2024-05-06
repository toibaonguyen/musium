
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserBackgroundImageRequest : BaseRequest
{
    [Required]
    public required IFormFile BackgroundImage { get; set; }
}