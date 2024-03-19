using System;
using MongoDB.Bson;
namespace JobNet.Models.Entities;
public class Message
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required ObjectId Sender { get; set; }
    public required ObjectId Receiver { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsImage { get; set; }
}
