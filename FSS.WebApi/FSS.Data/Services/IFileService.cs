using FSS.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FSS.Data.Services
{
    public interface IFileService
    {
        Task<FileRepsonseDto> UploadFileAsync(IFormFile file);
    }
}
