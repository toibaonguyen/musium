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
    public Task DeleteFileAsync(string uri)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFilesAsync(string[] uris)
    {
        throw new NotImplementedException();
    }

    public Task<BlobDTO?> DownloadFileAsync(string uri)
    {
        throw new NotImplementedException();
    }
    public async Task<BlobDTO?> UploadFileAsync(IFormFile blob, string uploadFileName)
    {
        var contentType = blob.ContentType;
        BlobDTO returnBlob = new();
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

            returnBlob.Uri = blobClient.Uri.OriginalString;
            returnBlob.Name = blobClient.Name;
            returnBlob.ContentType = blob.ContentType;
            return returnBlob;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured when uploading file!");
            throw;
        }
    }
    public async Task<IList<BlobDTO?>> UploadFilesAsync(IList<IFormFile> blobs, string BaseUploadName)
    {
        try
        {

            var files = blobs.ToArray();
            int length = blobs.Count;
            List<BlobDTO?> result = [];
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
    // private Task<BlobDTO?> UploadImageAsync(IFormFile blob)
    // {
    //     try
    //     {

    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Exception occured when uploading file!");
    //         throw;
    //     }
    // }
}