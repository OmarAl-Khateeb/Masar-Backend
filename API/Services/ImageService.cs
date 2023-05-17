using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Interfaces;
using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Core.Entities;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IUnitOfWork _unitOfWork;
        public ImageService(IOptions<CloudinarySettings> config, IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            
            
            if (environment.IsDevelopment())
            {
                var acc = new Account
                (
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );
                
            _cloudinary = new Cloudinary(acc);
            }
            else 
            {
                var acc = new Account
                (
                    Environment.GetEnvironmentVariable("CS_CloudName"),
                    Environment.GetEnvironmentVariable("CS_ApiKey"),
                    Environment.GetEnvironmentVariable("CS_ApiSecret")
                );
                
            _cloudinary = new Cloudinary(acc);
            }
        }

        public async Task<ImageUploadResult> AddImageAsync(IFormFile file, string location)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File  = new FileDescription(file.FileName, stream),
                    // Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = location
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<Document> UploadDocumentAsync(IFormFile file, string location){
            
            var uploadFile = await AddImageAsync(file, location);

            // Create a new Document entity.
            var document = new Document
            {
                Name = uploadFile.PublicId,
                DocumentUrl = uploadFile.SecureUrl.AbsoluteUri,
                DocumentType = "Transaction",
                Tags = "",
                Note = "Student Made Transaction,",
                // Student = await _unitOfWork.Repository<Student>().GetByIdAsync(StudentId)
            };

            // Save the Document entity to the database.
            _unitOfWork.Repository<Document>().Add(document);

            return document;
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}