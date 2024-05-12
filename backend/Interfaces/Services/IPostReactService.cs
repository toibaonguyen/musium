using JobNet.DTOs;
using JobNet.Enums;
using JobNet.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace JobNet.Interfaces.Services;

public interface IPostReactService
{
    Task ReactToPost(int userId, int PostId, PostReactionType reaction);
    Task DeteleReact(int userId, int PostId);
    // Task<List<PostDTO>> GetRandomActivePostDTOFromFriends(int UserId, int limit);
    // Task<List<PostDTO>> GetRandomActivePostDTOFromStranger(int UserId, int limit);
}