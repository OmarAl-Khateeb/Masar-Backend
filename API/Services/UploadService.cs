using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Core.Interfaces;
using Core.Entities;

namespace Infrastructure.Services
{
    public class UploadService : IUploadService
{
    private readonly IWebHostEnvironment _env;

    public UploadService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<UploadFile> UploadAsync(IFormFile file, string uploadPath)
    {
        if (file == null || file.Length == 0) throw new ArgumentException("Please select a file to upload.");

        string uploadsFolder = Path.Combine(_env.WebRootPath, uploadPath);

        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
    
        var uploadFile = new UploadFile
        {
            FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
        };
        uploadFile.FilePath = Path.Combine(uploadsFolder, uploadFile.FileName);

        using (var stream = new FileStream(uploadFile.FilePath, FileMode.Create)) await file.CopyToAsync(stream);

        return (uploadFile);
    }
}
}