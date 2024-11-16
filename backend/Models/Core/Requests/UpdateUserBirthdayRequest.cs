
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserBirthdayRequest : BaseRequest
{
    [Required]
    public required DateTime Birthday { get; set; }
}
