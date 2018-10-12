using FSS.Data.DbObjects;
using FSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using fdo = FSS.Data.DbObjects;

namespace FSS.Data.Repositories
{
    public interface IFileRepository : IEntityBaseRepository<fdo.File>
    {
        Task<PageItem<fdo.File>> GetPaginatedFiles(PagedRequestDto prd);
    }
}
