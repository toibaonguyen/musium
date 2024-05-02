
using JobNet.Data;
using JobNet.DTOs;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;
using SendGrid.Helpers.Errors.Model;

namespace JobNet.Services;

public class PostService : IPostService
{
    private readonly string USER_IS_NOT_EXIST = "User is not exist!";
    private readonly ILogger<PostService> _logger;
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly IFileService _fileService;
    private readonly IUserService _userService;
    public PostService(ILogger<PostService> logger, JobNetDatabaseContext databaseContext, IFileService fileService, IUserService userService)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _fileService = fileService;
        _userService = userService;
    }

    public Task<bool> CheckIfUserIsOwner(int UserId, int PostId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckIsOwner(int PostId, int OwnerId)
    {
        throw new NotImplementedException();
    }

    public async Task<PostDTO> CreateNewPost(CreatePostDTO post, int OwnerId)
    {
        try
        {
            User? user = await _userService.GetUserById(OwnerId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
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
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            await _databaseContext.Posts.AddAsync(newPost);
            await _databaseContext.SaveChangesAsync();
            return new PostDTO()
            {
                User = user.ToListUserDTO(),
                Id = newPost.Id,
                Content = newPost.Content,
                Images = newPost.Images,
                Videos = newPost.Videos,
                OtherFiles = newPost.OtherFiles,
                CreatedAt = newPost.CreatedAt,
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when creating new post");
            throw;
        }
    }

    public Task<PostDTO> GetActiveAndNotBanPostDTOById(int PostId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PostDTO>> SearchForActiveAndNotBanPostsDTOWithKeyword(string keyword)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePost(int PostId, JsonPatchDocument<Post> postUpdates)
    {
        throw new NotImplementedException();
    }
}