
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class CreatePostRequest : BaseRequest
{
    [Required]
    public required CreatePostDTO Data { get; set; }
}