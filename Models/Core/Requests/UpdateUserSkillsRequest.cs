
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserSkillsRequest : BaseRequest
{
    [Required]
    public required List<CreateSkillDTO> Skills { get; set; }
}
