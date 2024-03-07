using System;
using JobNet.Enums;
using MongoDB.Bson;
namespace JobNet.Models;
public class Experience
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required ObjectId Author { get; set; }
    public required string Title { get; set; }
    public required EmploymentType EmploymentType { get; set; }
    public required ObjectId Company { get; set; }
    public required string Location { get; set; }
    public required LocationType LocationType { get; set; }
    public required string Description { get; set; }
    public bool IsUserWorkingAt { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
