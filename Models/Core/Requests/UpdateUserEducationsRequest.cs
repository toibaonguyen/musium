using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserEducationsRequest : BaseRequest
{
    [Required]
    public required List<CreateEducationDTO> Educations { get; set; }
}