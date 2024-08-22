using Microsoft.AspNetCore.Http;

namespace RecipeSharingProject.Common.Interfaces;

public interface IUploadService
{
    Task DeleteFileAsync(string filePath);
    Task<byte[]> GetFileAsync(string filePath);
    string GetFileUrl (string filePath);
    Task<string> UploadFileAsync(IFormFile formFile);
}
