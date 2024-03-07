using System;
using MongoDB.Bson;
namespace JobNet.Models;
public class Industry
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string Name { get; set; }
}