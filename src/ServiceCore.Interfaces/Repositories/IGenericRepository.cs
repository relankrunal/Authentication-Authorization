using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ServiceCore.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool ignoreQueryFilters = false);

        Task<PagingList<T>> Search(
            Expression<Func<T, bool>> filter = null,
            int? pageSize = null,
            int pageNumber = 1,
            SortDirection sortDirection = SortDirection.Ascending,
            string sortField = "",
            bool IsInclude = false,
            bool ignoreQueryFilters = false);

        Task<T> GetById(object id);
        Task<T> Insert(T entity);
        Task<T> Update(T entityToUpdate);
        IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null, bool IsInclude = false, bool ignoreQueryFilters = false);
        Task<List<T>> UpdateRange(List<T> entitiesToUpdate);
        Task<List<T>> AddRange(List<T> entitiesToUpdate);
        Task<PagingList<T>> ExecuteStoredProcedure<I>(string procedureName, I input, string parameters);
        Task<List<List<dynamic>>> ExecuteStoredProcedureDataSet<I>(string procedureName, I input);
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class PagingList<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}
