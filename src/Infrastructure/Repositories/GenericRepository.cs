using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ServiceCore.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T, C> : IGenericRepository<T>
        where T : class
        where C : DbContext
    {
        protected readonly C _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(C context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);
            return await query.ToListAsync();
        }

        public async Task<PagingList<T>> Search(Expression<Func<T, bool>> filter = null,
            int? pageSize = null,
            int pageNumber = 1,
            SortDirection sortDirection = SortDirection.Ascending,
            string sortField = "",
            bool IsInclude = false,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            var items = await query.Skip((pageNumber - 1) * (pageSize ?? 10)).Take(pageSize ?? 10).ToListAsync();
            return new PagingList<T> { Items = items, TotalCount = await query.CountAsync() };
        }

        public async Task<T> GetById(object id) => await _dbSet.FindAsync(id);

        public async Task<T> Insert(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entityToUpdate;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null, bool IsInclude = false, bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            return query;
        }

        public async Task<List<T>> UpdateRange(List<T> entitiesToUpdate)
        {
            _dbSet.UpdateRange(entitiesToUpdate);
            await _context.SaveChangesAsync();
            return entitiesToUpdate;
        }

        public async Task<List<T>> AddRange(List<T> entitiesToUpdate)
        {
            await _dbSet.AddRangeAsync(entitiesToUpdate);
            await _context.SaveChangesAsync();
            return entitiesToUpdate;
        }

        public Task<PagingList<T>> ExecuteStoredProcedure<I>(string procedureName, I input, string parameters)
        {
            throw new NotImplementedException();
        }

        public Task<List<List<dynamic>>> ExecuteStoredProcedureDataSet<I>(string procedureName, I input)
        {
            throw new NotImplementedException();
        }
    }
}
