

using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class CreateJobPostRequest : BaseRequest
{
    [Required]
    public required CreateJobPostDTO Data { get; set; }
}