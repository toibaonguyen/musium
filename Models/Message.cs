using System;
using MongoDB.Bson;
namespace JobNet.Models;
public class Message
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string Sender { get; set; }
    public required string Receiver { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsImage { get; set; }
}
