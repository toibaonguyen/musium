using System;
using MongoDB.Bson;
namespace JobNet.Models;
public class Education
{
    //may be i will change later
    public ObjectId? Id { get; set; }
    public required string SchoolName { get; set; }
    public required string Degree { get; set; }
    public required string FieldOfStudy { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Grade { get; set; }
    public string? ActivitiesAndSocieties { get; set; }
    public string? Description { get; set; }
}