
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class CreateSkillRequest : BaseRequest
{
    [Required]
    public required CreateSkillDTO Data { get; set; }
}