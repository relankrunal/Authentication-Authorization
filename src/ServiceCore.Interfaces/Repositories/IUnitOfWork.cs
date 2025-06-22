using Microsoft.EntityFrameworkCore;

namespace ServiceCore.Interfaces.Repositories
{
    public interface IUnitOfWork<C> where C : DbContext
    {
        void SaveChanges();
        IGenericRepository<T> GetRepository<T>() where T : class;
        Task<int?> ExecuteStoredProcedure<I>(string query, I input, string output = "", bool forJob = false);
        Task<dynamic> ExecuteStoredProcedure<I, O>(string procedureName, I input, string output = null) where O : class;
        Task<bool?> ExecuteStoredProcedureWithBooleanResult<I>(string procedureName, I input);
    }
}
