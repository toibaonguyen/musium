using JobNet.DTOs;
using JobNet.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace JobNet.Interfaces.Services;

public interface IPostService
{
    Task<Post?> GetPostById(int PostId);
    Task<PostDTO> CreateNewPostAndSendToticationToConnections(CreatePostDTO post, int OwnerId);
    Task<PostDTO> UpdatePost(int PostId, UpdatePostDTO postUpdates);
    Task<PostDTO?> GetActivePostDTOById(int PostId);
    Task<List<PostDTO>> SearchForActivePostDTOsWithKeyword(string keyword, int limit);
    Task<bool> CheckIfUserIsOwner(int UserId, int PostId);
    Task DisablePost(int PostId);
    Task<List<PostDTO>> GetRandomActivePostDTOs(int limit);
    // Task<List<PostDTO>> GetRandomActivePostDTOFromStranger(int UserId, int limit);
}