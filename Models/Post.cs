using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace JobNet.Models;
public class Post
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required ObjectId Author { get; set; }
    public required string Content { get; set; }
    public string[]? Images { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }
}