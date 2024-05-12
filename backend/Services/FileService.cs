using System.Net.Http.Headers;
using System.Net.Mime;
using Azure.Storage.Blobs;
using JobNet.DTOs;
using JobNet.Exceptions;
using JobNet.Interfaces.Services;
using JobNet.Settings;
using Microsoft.Extensions.Options;
using Npgsql.Replication.PgOutput.Messages;

namespace JobNet.Services;

public class FileService : IFileService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<FileService> _logger;
    public FileService(ILogger<FileService> logger, IOptions<AzureSetting> azureSettingOptions)
    {
        _blobServiceClient = new(azureSettingOptions.Value.ConnectionString);
        _logger = logger;
    }
    public async Task DeleteFileAsync(string blobUri)
    {
        try
        {
            var blobServiceClient = _blobServiceClient;

            // Tách URI Blob thành container name và blob name
            var uri = new Uri(blobUri);
            string containerName = uri.Segments[1]; // Là phần sau phần scheme và host
            string blobName = blobUri.Replace(uri.Scheme + "://" + uri.Host + "/", "");

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            // Xóa Blob
            await blobClient.DeleteIfExistsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured when deleting file!");
            throw;
        }
    }

    public async Task DeleteFilesAsync(string[] uris)
    {
        try
        {
            foreach (string uri in uris)
            {
                await DeleteFileAsync(uri);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured when deleting files!");
            throw;
        }
    }

    public Task<BlobDTO?> DownloadFileAsync(string uri)
    {
        throw new NotImplementedException();
    }
    public async Task<BlobDTO> UploadFileAsync(IFormFile blob, string uploadFileName)
    {
        var contentType = blob.ContentType;
        BlobContainerClient containerClient;

        try
        {
            if (contentType.Contains("image"))
            {
                containerClient = _blobServiceClient.GetBlobContainerClient("photos");
                await containerClient.CreateIfNotExistsAsync();
            }
            else if (contentType.Contains("video"))
            {
                containerClient = _blobServiceClient.GetBlobContainerClient("videos");
                await containerClient.CreateIfNotExistsAsync();
            }
            else
            {
                containerClient = _blobServiceClient.GetBlobContainerClient("files");
                await containerClient.CreateIfNotExistsAsync();
            }

            BlobClient blobClient = containerClient.GetBlobClient(uploadFileName);

            await using (Stream? data = blob.OpenReadStream())
            {
                await blobClient.UploadAsync(data);
            }
            BlobDTO returnBlob = new()
            {
                Uri = blobClient.Uri.OriginalString,
                Name = blobClient.Name,
                ContentType = blob.ContentType
            };
            return returnBlob;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured when uploading file!");
            throw;
        }
    }
    public async Task<IList<BlobDTO>> UploadFilesAsync(IList<IFormFile> blobs, string BaseUploadName)
    {
        try
        {

            var files = blobs.ToArray();
            int length = blobs.Count;
            List<BlobDTO> result = [];
            for (int i = 0; i < length; i++)
            {
                IFormFile blob = blobs[i];
                var fileName = ContentDispositionHeaderValue.Parse(blob.ContentDisposition).FileName?.Trim('"') ?? throw new BadRequestException("Missing file!");
                string fileExtension = Path.GetExtension(fileName);
                result.Add(await UploadFileAsync(blob, $"{BaseUploadName}-S{i}-S{Guid.NewGuid()}{fileExtension}"));
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured when uploading files!");
            throw;
        }
    }
}