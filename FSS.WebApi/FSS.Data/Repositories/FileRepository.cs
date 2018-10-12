using FSS.Data.Data;
using FSS.Data.DbObjects;
using FSS.Data.Models;
using FSS.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using fdo = FSS.Data.DbObjects;
namespace FSS.Data.Repositories
{
    public class FileRepository : EntityBaseRepository<fdo.File>, IFileRepository
    {
        public FileRepository(ApplicationDbContext context)
            : base(context)
        { }
        public override void Add(fdo.File entity)
        {
            base.Add(entity);
        }

        public override IEnumerable<fdo.File> AllIncluding(params Expression<Func<fdo.File, object>>[] includeProperties)
        {
            return base.AllIncluding(includeProperties);
        }

        public override Task<IEnumerable<fdo.File>> AllIncludingAsync(params Expression<Func<fdo.File, object>>[] includeProperties)
        {
            return base.AllIncludingAsync(includeProperties);
        }

        public override void Commit()
        {
            base.Commit();
        }

        public override void Delete(fdo.File entity)
        {
            base.Delete(entity);
        }

        public override void Edit(fdo.File entity)
        {
            base.Edit(entity);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override IEnumerable<fdo.File> FindBy(Expression<Func<fdo.File, bool>> predicate)
        {
            return base.FindBy(predicate);
        }

        public override Task<IEnumerable<fdo.File>> FindByAsync(Expression<Func<fdo.File, bool>> predicate)
        {
            return base.FindByAsync(predicate);
        }

        public override IEnumerable<fdo.File> GetAll()
        {
            return base.GetAll();
        }

        public override Task<IEnumerable<fdo.File>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public async Task<PageItem<File>> GetPaginatedFiles(PagedRequestDto prd)
        {
            IQueryable<File> query = null; 

            var exp = ExpressionBuilder.GetExpression<File>(prd.Filters);
            query = query.Where(exp).AsQueryable();

            var pageItem = new PageItem<File>();

            pageItem.Items = await query.Take(prd.PageNo * prd.PageLen)
                                    .Skip((prd.PageNo - 1) * prd.PageLen)
                                    .ToListAsync();

            pageItem.TotalItems = query.Count();

            return pageItem;
        }

    }
}

