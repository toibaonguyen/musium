using JobNet.Enums;
using JobNet.Interfaces.Services;

namespace JobNet.Services;

public class ConnectionService : IConnectionService
{
    public Task CreateConnection(int senderId, int recieverId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateConnectionRequestStatus(int connectionId, ConnectionRequestStatusType status)
    {
        throw new NotImplementedException();
    }
}