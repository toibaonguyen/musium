using JobNet.Data;
using JobNet.DTOs;
using JobNet.Exceptions;
using JobNet.Extensions;
using JobNet.Hubs;
using JobNet.Interfaces.Hubs;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace JobNet.Services;

public class PrivateChatService : IPrivateChatService
{
    private readonly string INVALID_USER = "Invalid user";
    private readonly string INVALID_CONVERSATION = "Invalid conversation";
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly IUserService _userService;
    private readonly IHubContext<PrivateChatHub, IPrivateChatListenerHub> _chatHubContext;
    private readonly IFileService _fileService;

    public PrivateChatService(JobNetDatabaseContext databaseContext, IUserService userService, IHubContext<PrivateChatHub, IPrivateChatListenerHub> chatHubContext, IFileService fileService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _chatHubContext = chatHubContext;
        _fileService = fileService;
    }
    public async Task<bool> CheckIfUserIsInConversation(int userId, int conversationId)
    {
        try
        {
            var user = await _databaseContext.UserInConversations.FirstOrDefaultAsync(e => e.ConversationId == conversationId && e.UserId == userId);
            if (user == null) return false;
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<ConversationDTO>> GetConversationBoxsOfUserOrderByLastMessageSentTimeDesc(int userId, int limit, DateTime cursor)
    {
        try
        {
            User? user = await _userService.GetUserById(userId) ?? throw new BadRequestException(INVALID_USER);
            var userInConversation = await _databaseContext.UserInConversations.Where(u => u.UserId == userId).ToListAsync();
            return userInConversation.Select(e => e.Conversation).ToList().OrderByDescending(e => e.Messages.OrderByDescending(t => t.CreatedAt).First().CreatedAt).Where(e => e.Messages.OrderByDescending(t => t.CreatedAt).First().CreatedAt <= cursor).Select(e => e.ToConversationDTO(e.Users.First(u => u.UserId != userId).User.ToChatUserDTO())).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<MessageDTO>> GetMessagesOfConversationOrderBySentTimeDesc(int conversationId, int limit, DateTime cursor)
    {
        try
        {
            var conversation = await _databaseContext.Conversations.FindAsync(conversationId) ?? throw new BadRequestException(INVALID_CONVERSATION);
            return conversation.Messages.Where(e => e.CreatedAt <= cursor).Take(limit).Select(e => e.ToMessageDTO()).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task SendPrivateMessage(int SenderId, int RecieverId, CreateMessageDTO message)
    {
        try
        {
            var sender = await _userService.GetUserById(SenderId) ?? throw new BadRequestException(INVALID_USER);
            var reciever = await _userService.GetUserById(RecieverId) ?? throw new BadRequestException(INVALID_USER);
            if (!sender.IsActive || !reciever.IsActive)
            {
                throw new BadRequestException(INVALID_USER);
            }
            var savedConversation = await _databaseContext.Conversations.FirstOrDefaultAsync(e => e.Users.Where(u => u.UserId == SenderId || u.UserId == RecieverId).Count() == 2);
            Message newMessage = new()
            {
                Sender = sender,
                Content = message.Content,
                Image = message.Image != null ? (await _fileService.UploadFileAsync(message.Image, $"{sender.Id}-image-{Guid.NewGuid()}-{new DateTime()}")).Uri : null,
                Video = message.Video != null ? (await _fileService.UploadFileAsync(message.Video, $"{sender.Id}-video-{Guid.NewGuid()}-{new DateTime()}")).Uri : null,
                OtherFile = message.OtherFile != null ? (await _fileService.UploadFileAsync(message.OtherFile, $"{sender.Id}-otherfile-{Guid.NewGuid()}-{new DateTime()}")).Uri : null,

            };
            if (savedConversation == null)
            {
                Conversation conversation = new();
                UserInConversation senderInConversation = new()
                {
                    User = sender,
                    Conversation = conversation
                };
                UserInConversation recieverInConversation = new()
                {
                    User = reciever,
                    Conversation = conversation
                };

                await _databaseContext.Conversations.AddAsync(conversation);
                await _databaseContext.SaveChangesAsync();
                await _databaseContext.UserInConversations.AddAsync(senderInConversation);
                await _databaseContext.UserInConversations.AddAsync(recieverInConversation);
                await _databaseContext.SaveChangesAsync();
                newMessage.Conversation = conversation;
                await _databaseContext.Messages.AddAsync(newMessage);
                await _databaseContext.SaveChangesAsync();
            }
            else
            {
                newMessage.Conversation = savedConversation;
                await _databaseContext.Messages.AddAsync(newMessage);
                await _databaseContext.SaveChangesAsync();
            }
            await _chatHubContext.Clients.User(RecieverId.ToString()).RecieveMessage(newMessage.ToMessageDTO());
        }
        catch (Exception)
        {
            throw;
        }
    }
}