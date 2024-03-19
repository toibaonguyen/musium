using System;
using MongoDB.Bson;
namespace JobNet.Models.Entities;
public class AdminAccount
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string IsActive { get; set; }
}