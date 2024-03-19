using MongoDB.Bson;

namespace JobNet.DTOs;

public class AccountDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

