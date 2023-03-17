using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Core.Entities;
//yes i know this is in the wrong folder but i give up
namespace Core.Interfaces
{
    public interface IUploadService
    {
        Task<UploadFile> UploadAsync(IFormFile file, string uploadPath);
    }
}