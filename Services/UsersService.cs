using Microsoft.Extensions.Options;
using MongoDB.Driver;
using JobNet.Models;
using JobNet.Data;


namespace JobNet.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;
    public UsersService(IOptions<JobNetDatabaseSettings> jobNetDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            jobNetDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(
            jobNetDatabaseSettings.Value.DatabaseName);
        this._usersCollection = mongoDatabase.GetCollection<User>(jobNetDatabaseSettings.Value.UsersCollectionName);
    }
}