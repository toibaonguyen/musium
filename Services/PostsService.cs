using Microsoft.Extensions.Options;
using JobNet.Models;
using JobNet.Settings;
using JobNet.Data;
using Microsoft.EntityFrameworkCore;
using JobNet.DTOs;


namespace JobNet.Services;

public class PostsService
{
    private readonly JobNetDatabaseContext _databaseContext;
    public PostsService(JobNetDatabaseContext databaseContext)
    {
        this._databaseContext = databaseContext;
    }
    // public async Task<PostDTO> CreateNewPost(CreatePostDTO newPost)
    // {

    // }
    // public async Task<PostDTO> GetPostByID(string id)
    // {

    // }
    // public async Task<PostDTO> UpdatePatchPost(string id)
    // {

    // }
    // public async Task DisablePost(string id)
    // {

    // }
}