using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class FileService : IFileService
    {
        public async Task<string> UploadFileAsync(string folderName, IFormFile file)
        {
            ValidateFile(file);

            // 1) Get Directory
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            // 2) Create folder if not exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 3) Get File Name
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            string fileName = Guid.NewGuid() + extension;

            // 4) Merge Path with File Name
            string fullPath = Path.Combine(folderPath, fileName);

            // 5) Save File As Streams
            await using var stream = new FileStream(
                fullPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            await file.CopyToAsync(stream);

            return fileName;
        }

        public Task<bool> DeleteFileAsync(string folderName, string fileName)
        {
            // 1) Get Full Path
            string fullPath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

            // 2) Check if file exists
            if (!File.Exists(fullPath))
            {
                return Task.FromResult(false);
            }

            // 3) Delete File
            File.Delete(fullPath);

            return Task.FromResult(true);
        }

        public bool FileExists(string folderName, string fileName)
        {
            string fullPath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

            return File.Exists(fullPath);
        }

        private static void ValidateFile(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or null.");
            }
        }
    }
}
