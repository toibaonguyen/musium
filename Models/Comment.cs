using System;
using MongoDB.Bson;
namespace JobNet.Models;
public class Comment
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required ObjectId Post { get; set; }
    public required ObjectId Author { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}