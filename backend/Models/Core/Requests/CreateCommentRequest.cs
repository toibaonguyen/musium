


using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class CreateCommentRequest : BaseRequest
{
    [Required]
    public required CreatePostCommentDTO Data { get; set; }
}
