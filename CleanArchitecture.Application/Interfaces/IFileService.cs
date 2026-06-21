using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(string folderName, IFormFile file);
        Task<bool> DeleteFileAsync(string folderName, string fileName);
        bool FileExists(string folderName, string fileName);
    }
}
