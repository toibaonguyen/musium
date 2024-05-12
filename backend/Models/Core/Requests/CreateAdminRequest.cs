


using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class CreateAdminRequest : BaseRequest
{
    [Required]
    public required CreateAdminDTO Data { get; set; }
}