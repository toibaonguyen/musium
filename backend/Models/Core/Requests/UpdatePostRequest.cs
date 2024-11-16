using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;


public class UpdatePostRequest : BaseRequest
{
    [Required]
    public required PostDTO Data { get; set; }
}