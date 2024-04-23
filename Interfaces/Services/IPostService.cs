using JobNet.DTOs;
using JobNet.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace JobNet.Interfaces.Services;

public interface IPostService
{
    Task<bool> CheckIsOwner(int PostId, int OwnerId);
    Task<PostDTO> CreateNewPost(CreatePostDTO post, int OwnerId);
    Task UpdatePost(int PostId, JsonPatchDocument<Post> postUpdates);
    Task<PostDTO> GetActiveAndNotBanPostDTOById(int PostId);
    Task<List<PostDTO>> SearchForActiveAndNotBanPostsDTOWithKeyword(string keyword);
    Task<bool> CheckIfUserIsOwner(int UserId, int PostId);
}