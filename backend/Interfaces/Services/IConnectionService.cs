using JobNet.Enums;

namespace JobNet.Interfaces.Services;

public interface IConnectionService
{
    Task CreateConnection(int senderId, int recieverId);
    Task UpdateConnectionRequestStatus(int connectionId, ConnectionRequestStatusType status);
    Task<bool> CheckIfIsConnected(int userId1, int userId2);

}