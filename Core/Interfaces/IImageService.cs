using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file, string location);
        Task<DeletionResult> DeleteImageAsync(string publicId);
        Task<Document> UploadDocumentAsync(IFormFile file, string location);
    }
}