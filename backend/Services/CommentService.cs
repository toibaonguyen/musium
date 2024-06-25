using StackExchange.Redis;
using JobNet.DTOs;
using JobNet.Interfaces.Services;
using JobNet.Exceptions;
using JobNet.Utilities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using JobNet.Contants;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using JobNet.Models.Core.Common;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using JobNet.Settings;
using Microsoft.Extensions.Options;
using JobNet.Models.Core.Responses;
using JobNet.Extensions;
using JobNet.Data;
using JobNet.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobNet.Services;

public class CommentService : ICommentService
{
    private readonly string INVALID_POST = "Invalid post!";
    private readonly string INVALID_USER = "Invalid user!";
    private readonly string INVALID_COMMENT = "Invalid valid!";
    ILogger<CommentService> _logger;
    IFileService _fileService;
    JobNetDatabaseContext _databaseContext;
    IUserService _userService;
    IPostService _postService;
    public CommentService(JobNetDatabaseContext databaseContext, ILogger<CommentService> logger, IFileService fileService, IPostService postService, IUserService userService)
    {
        _logger = logger;
        _fileService = fileService;
        _databaseContext = databaseContext;
        _postService = postService;
        _userService = userService;
    }
    public async Task<CommentDTO> CommentToPost(int userId, int postId, CreatePostCommentDTO comment)
    {
        try
        {
            var post = await _postService.GetPostById(postId);
            if (post == null || !post.IsActive)
            {
                throw new BadRequestException(INVALID_POST);
            }
            var user = await _userService.GetUserById(postId);
            if (user == null || !user.IsActive)
            {
                throw new BadRequestException(INVALID_USER);
            }
            Comment newComment = new Comment
            {
                UserId = userId,
                PostId = postId,
                Content = comment.Content,
                Image = comment.Image != null ? (await _fileService.UploadFileAsync(comment.Image, $"{userId}-{Guid.NewGuid()}-{new DateTime()}")).Uri : null
            };
            await _databaseContext.Comments.AddAsync(newComment);
            await _databaseContext.SaveChangesAsync();

            return newComment.ToCommentDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<CommentDTO>> GetCommentsOfPost(int postId, int limit, DateTime cursor)
    {
        try
        {
            var post = await _postService.GetPostById(postId);
            if (post == null || !post.IsActive)
            {
                throw new BadRequestException(INVALID_POST);
            }
            List<CommentDTO> Comments = await _databaseContext.Comments.Where(c => c.PostId == postId && c.CreatedAt <= cursor).OrderByDescending(e => e.CreatedAt).Take(limit).Select(e => e.ToCommentDTO()).ToListAsync();

            return Comments;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CommentDTO> UpdateComment(int commentId, CreatePostCommentDTO comment)
    {
        try
        {
            var targetComment = await _databaseContext.Comments.FindAsync(commentId) ?? throw new BadRequestException(INVALID_COMMENT);
            targetComment.Content = comment.Content;
            targetComment.Image = comment.Image != null ? (await _fileService.UploadFileAsync(comment.Image, $"{targetComment.UserId}-{Guid.NewGuid()}-{new DateTime()}")).Uri : null;
            targetComment.UpdatedAt = DateTime.Now;
            await _databaseContext.SaveChangesAsync();
            return targetComment.ToCommentDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
}