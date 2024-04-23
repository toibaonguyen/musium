
namespace JobNet.DTOs;

public class ProfileUserDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string BackgroundImage { get; set; }
    public required string Email { get; set; }
    public required string Location { get; set; }
    public DateTime Birthday { get; set; }
    public required IEnumerable<CertificationDTO> Certifications { get; set; }
    public required IEnumerable<EducationDTO> Educations { get; set; }
    public required IEnumerable<ExperienceDTO> Experiences { get; set; }
    public required IEnumerable<SkillDTO> Skills { get; set; }
    public bool? IsHiring { get; set; }
}

