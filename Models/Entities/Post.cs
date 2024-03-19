using System;
using MongoDB.Bson;
namespace JobNet.Models.Entities;
public class Post
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required ObjectId Author { get; set; }
    public required string Content { get; set; }
    public required IEnumerable<string> Images { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }
}