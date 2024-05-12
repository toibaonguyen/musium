
using System.ComponentModel.DataAnnotations;
using JobNet.DTOs;

namespace JobNet.Models.Core.Requests;

public class UpdateUserCertificationsRequest : BaseRequest
{
    [Required]
    public required List<CreateCertificationDTO> Certifications { get; set; }
}
