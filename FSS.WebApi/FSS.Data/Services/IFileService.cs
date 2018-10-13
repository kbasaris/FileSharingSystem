using FSS.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using dbo = FSS.Data.DbObjects;

namespace FSS.Data.Services
{
    public interface IFileService
    {
        Task<FileRepsonseDto> UploadFileAsync(IFormFile file);
        Task<PageItem<dbo.File>> GetPaginatedFiles(PagedRequestDto prd);
    }
}
