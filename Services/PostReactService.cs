using JobNet.Data;
using JobNet.Enums;
using JobNet.Exceptions;
using JobNet.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace JobNet.Services;

public class PostReactService : IPostReactService
{
    private readonly string INVALID_USER = "Invalid user!";
    private readonly string INVALID_POST = "Invalid post!";
    private readonly string INVALID_REACT = "Invalid reaction!";
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly IPostService _postService;
    private readonly IUserService _userService;
    public PostReactService(JobNetDatabaseContext databaseContext, IPostService postService, IUserService userService)
    {
        _databaseContext = databaseContext;
        _postService = postService;
        _userService = userService;
    }
    public async Task DeteleReact(int userId, int PostId)
    {
        try
        {
            var post = await _postService.GetPostById(PostId) ?? throw new BadRequestException(INVALID_POST);
            var user = await _userService.GetUserById(userId) ?? throw new BadRequestException(INVALID_USER);
            var postReaction = await _databaseContext.PostReactions.FirstOrDefaultAsync(e => e.UserId == userId && e.PostId == PostId) ?? throw new BadRequestException(INVALID_REACT);
            _databaseContext.PostReactions.Remove(postReaction);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task ReactToPost(int userId, int PostId, PostReactionType reaction)
    {
        try
        {
            var post = await _postService.GetPostById(PostId) ?? throw new BadRequestException(INVALID_POST);
            var user = await _userService.GetUserById(userId) ?? throw new BadRequestException(INVALID_USER);
            var postReaction = await _databaseContext.PostReactions.FirstOrDefaultAsync(e => e.UserId == userId && e.PostId == PostId) ?? throw new BadRequestException(INVALID_REACT);
            postReaction.React = reaction;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}