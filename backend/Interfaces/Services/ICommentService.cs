using JobNet.DTOs;
using JobNet.Enums;

namespace JobNet.Interfaces.Services;

public interface ICommentService
{
    Task<CommentDTO> CommentToPost(int userId, int postId, CreatePostCommentDTO comment);
    Task<CommentDTO> UpdateComment(int commentId, CreatePostCommentDTO comment);
    Task<List<CommentDTO>> GetCommentsOfPost(int postId, int limit, DateTime cursor);

}