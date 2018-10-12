using System;
using System.Collections.Generic;
using System.Text;
using fdo = FSS.Data.DbObjects;

namespace FSS.Data.Repositories
{
    public interface IFileRepository : IEntityBaseRepository<fdo.File>
    {
    }
}
