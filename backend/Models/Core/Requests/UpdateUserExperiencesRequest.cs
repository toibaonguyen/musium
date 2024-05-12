
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserExperiencesRequest : BaseRequest
{
    [Required]
    public required List<CreateExperienceDTO> Experiences { get; set; }
}
