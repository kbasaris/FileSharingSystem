using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FSS.Data.Models;
using FSS.Data.Repositories;
using Microsoft.AspNetCore.Http;
using dbo = FSS.Data.DbObjects;
namespace FSS.Data.Services
{
    public class FileService : IFileService
    {
        private IFileRepository _fileRepository = null;
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<FileRepsonseDto> UploadFileAsync(IFormFileCollection files)
        {
            var rsp = new FileRepsonseDto();
            foreach(IFormFile f in files)
            {
                var rst = await UploadFileAsync(f);
                rsp.Message = "File Uploaded Successfully";
                rsp.Status = StatusEnum.Success;
            }
            return rsp;
        }
        public async Task<FileRepsonseDto> UploadFileAsync(IFormFile file)
        {
            var resp = new FileRepsonseDto();
            return await SaveFile(file, resp);
        }

        private async Task<FileRepsonseDto> SaveFile(IFormFile file, FileRepsonseDto resp)
        {
            try
            {
                var newFile = new dbo.File();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                newFile.Name = file.Name;
                newFile.Path = path;
                _fileRepository.Add(newFile);
                _fileRepository.Commit();

                resp.Message = "File Uploaded successfully";
                resp.Status = StatusEnum.Success;

            }
            catch (Exception e)
            {
                resp.Status = StatusEnum.Error;
                resp.Message = e.Message;
            }
            return resp;
        }

        public async Task<PageItem<dbo.File>> GetPaginatedFiles(PagedRequestDto prd)
        {
            return await _fileRepository.GetPaginatedFiles(prd);
        }
    }
}
