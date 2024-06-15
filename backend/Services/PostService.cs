
using JobNet.Data;
using JobNet.DTOs;
using JobNet.Enums;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
namespace JobNet.Services;

public class PostService : IPostService
{
    private readonly string USER_IS_NOT_EXIST = "User is invalid!";
    private readonly string POST_IS_NOT_EXIST = "Post is invalid!";
    private readonly ILogger<PostService> _logger;
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly IFileService _fileService;
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;
    private readonly IConnectionService _connectionService;
    public PostService(ILogger<PostService> logger, JobNetDatabaseContext databaseContext, IFileService fileService, IUserService userService, INotificationService notificationService, IConnectionService connectionService)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _fileService = fileService;
        _userService = userService;
        _notificationService = notificationService;
        _connectionService = connectionService;
    }

    public async Task<Post?> GetPostById(int id)
    {
        try
        {
            return await _databaseContext.Posts.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when get post");
            throw;
        }
    }
    public async Task<bool> CheckIfUserIsOwner(int UserId, int PostId)
    {
        try
        {
            return await _databaseContext.Posts.AnyAsync(p => p.Id == PostId && p.OwnerId == UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when checking post's owner");
            throw;
        }
    }


    public async Task<PostDTO> CreateNewPostAndSendToticationToConnections(CreatePostDTO post, int OwnerId)
    {
        try
        {
            User? user = await _userService.GetUserById(OwnerId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            Post newPost = new()
            {
                OwnerId = user.Id,
                Owner = user,
                Content = post.Content,
                Images = (await _fileService.UploadFilesAsync(post.Images, $"{OwnerId}-{Guid.NewGuid()}-{new DateTime()}")).Where(x => x is not null).Select(e => e.Uri).Where(x => x is not null).ToList(),
                Videos = (await _fileService.UploadFilesAsync(post.Videos, $"{OwnerId}-{Guid.NewGuid()}-{new DateTime()}")).Where(x => x is not null).Select(e => e.Uri).Where(x => x is not null).ToList(),
                OtherFiles = (await _fileService.UploadFilesAsync(post.OtherFiles, $"{OwnerId}-{Guid.NewGuid()}-{new DateTime()}")).Where(x => x is not null).Select(e => e.Uri).Where(x => x is not null).ToList(),
                IsActive = true
            };

            await _databaseContext.Posts.AddAsync(newPost);
            await _databaseContext.SaveChangesAsync();
            List<int> userIds = await _databaseContext.Users.Select(u => u.Id).ToListAsync();
            List<int> connectedUserIds = [];
            foreach (int userId in userIds)
            {
                if (await _connectionService.CheckIfIsConnected(OwnerId, userId))
                {
                    connectedUserIds.Add(userId);
                }
            }
            try
            {
                await _notificationService.CreateAndSendNotification(ResourceNotificationType.POST, [.. connectedUserIds], $"{user.Name} posted new post!", newPost.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not create and sent message!");
            }
            return newPost.ToPostDTO();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when creating new post!");
            throw;
        }
    }

    public async Task<PostDTO?> GetActivePostDTOById(int PostId)
    {
        try
        {
            return (await _databaseContext.Posts.FirstOrDefaultAsync(x => x.Id == PostId && x.IsActive))?.ToPostDTO();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when getting active and not ban post by id");
            throw;
        }
    }

    public async Task<List<PostDTO>> SearchForActivePostDTOsWithKeyword(string keyword, int limit)
    {
        try
        {
            var posts = await _databaseContext.Posts.Where(x => x.Content.Contains(keyword)).OrderBy(e => EF.Functions.Random()).Take(limit).ToListAsync();
            return posts.Select(e => e.ToPostDTO()).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when updating post");
            throw;
        }
    }

    public async Task<PostDTO> UpdatePost(int PostId, UpdatePostDTO postUpdates)
    {
        try
        {
            var post = await GetPostById(PostId) ?? throw new BadRequestException(POST_IS_NOT_EXIST);
            await _fileService.DeleteFilesAsync([.. post.Images]);
            await _fileService.DeleteFilesAsync([.. post.Videos]);
            await _fileService.DeleteFilesAsync([.. post.OtherFiles]);
            post.Content = postUpdates.Content;
            post.Images = (await _fileService.UploadFilesAsync(postUpdates.Images, $"{post.OwnerId}-{Guid.NewGuid()}-{new DateTime()}")).Where(x => x is not null).Select(e => e.Uri).Where(x => x is not null).ToList();
            post.Videos = (await _fileService.UploadFilesAsync(postUpdates.Videos, $"{post.OwnerId}-{Guid.NewGuid()}-{new DateTime()}")).Where(x => x is not null).Select(e => e.Uri).Where(x => x is not null).ToList();
            post.OtherFiles = (await _fileService.UploadFilesAsync(postUpdates.OtherFiles, $"{post.OwnerId}-{Guid.NewGuid()}-{new DateTime()}")).Where(x => x is not null).Select(e => e.Uri).Where(x => x is not null).ToList();
            await _databaseContext.SaveChangesAsync();
            return post.ToPostDTO();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when updating post");
            throw;
        }
    }

    public async Task DisablePost(int PostId)
    {
        try
        {
            var post = await GetPostById(PostId) ?? throw new BadRequestException(POST_IS_NOT_EXIST);
            post.IsActive = false;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when updating post");
            throw;
        }
    }

    public async Task<List<PostDTO>> GetRandomActivePostDTOs(int limit)
    {
        try
        {
            return await _databaseContext.Posts.OrderBy(e => EF.Functions.Random()).Where(e => e != null).Take(limit).Select(e => e.ToPostDTO()).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when updating post");
            throw;
        }
    }
}