using JobNet.Data;
using JobNet.Enums;
using JobNet.Exceptions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;

namespace JobNet.Services;

public class ConnectionService : IConnectionService
{
    private readonly string INVALID_USER = "Invalid user!";
    private readonly string INVALID_CONNECTION_REQUEST = "Can request for request connection from this user!";
    private readonly string DO_NOT_EXIST_CONNECTION_REQUEST = "Do not exist connection request!";
    private readonly ILogger<ConnectionService> _logger;
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly IUserService _userService;
    public ConnectionService(ILogger<ConnectionService> logger, JobNetDatabaseContext databaseContext, IUserService userService)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _userService = userService;
    }

    public Task<bool> CheckIfIsConnected(int userId1, int userId2)
    {
        throw new NotImplementedException();
    }

    public async Task CreateConnectionAndSendNotificationToReciever(int senderId, int recieverId)
    {
        try
        {
            var user = await _databaseContext.Users.FindAsync(senderId);
            var destination = await _databaseContext.Users.FindAsync(recieverId);
            if (user == null || destination == null)
            {
                throw new BadRequestException(INVALID_USER);
            }
            bool isRequestable = _databaseContext.Connections.Where(c => (c.SenderId == senderId && c.RecieverId == recieverId) || (c.RecieverId == senderId && c.SenderId == recieverId)).OrderByDescending(e => e.UpdatedAt).ToList().FirstOrDefault()?.Status != ConnectionRequestStatusType.ACCEPT;
            if (!isRequestable)
            {
                throw new BadRequestException(INVALID_CONNECTION_REQUEST);
            }
            await _databaseContext.Connections.AddAsync(new Connection { Sender = user, Reciever = destination });
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateConnectionRequestStatus(int connectionId, ConnectionRequestStatusType status)
    {
        try
        {
            var connectionRequest = await _databaseContext.Connections.FindAsync(connectionId) ?? throw new BadRequestException(DO_NOT_EXIST_CONNECTION_REQUEST);
            connectionRequest.Status = status;
            connectionRequest.UpdatedAt = DateTime.Now;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}