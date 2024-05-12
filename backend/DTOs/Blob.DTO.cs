namespace JobNet.DTOs;

public class BlobDTO
{
    public required string Uri { get; set; }
    public required string Name { get; set; }
    public required string ContentType { get; set; }
}