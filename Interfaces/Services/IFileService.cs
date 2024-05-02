

using JobNet.DTOs;

namespace JobNet.Interfaces.Services;

public interface IFileService
{
    Task<BlobDTO?> UploadFileAsync(IFormFile blob, string uploadFileName);
    Task<IList<BlobDTO?>> UploadFilesAsync(IList<IFormFile> blobs, string BaseUploadName);
    Task<BlobDTO?> DownloadFileAsync(string uri);
    Task DeleteFileAsync(string uri);
    Task DeleteFilesAsync(string[] uris);
}