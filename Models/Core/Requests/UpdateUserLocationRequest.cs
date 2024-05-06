
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserLocationRequest : BaseRequest
{
    [Required]
    public required string Location { get; set; }
}
