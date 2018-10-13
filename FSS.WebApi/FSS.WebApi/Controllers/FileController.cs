using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSS.Data.Models;
using FSS.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dbo = FSS.Data.DbObjects;

namespace FSS.WebApi.Controllers
{
   
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok();
        }
        [HttpPost("Upload")]
        [AllowAnonymous]
        public async Task<FileRepsonseDto> UploadFile(IFormFile file)
        {
            return await _fileService.UploadFileAsync(file);
        }

        [HttpPost("GetFiles")]
        public async Task<PageItem<dbo.File>> GetFiles(PagedRequestDto prd)
        {
            return await _fileService.GetPaginatedFiles(prd);
        }
    }
}