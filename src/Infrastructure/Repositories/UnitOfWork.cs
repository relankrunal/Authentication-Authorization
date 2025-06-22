using Microsoft.EntityFrameworkCore;
using ServiceCore.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class UnitOfWork<C> : IUnitOfWork<C> where C : DbContext
    {
        private readonly C _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(C context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
                return (IGenericRepository<T>)_repositories[typeof(T)];

            var repo = new GenericRepository<T, C>(_context);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<int?> ExecuteStoredProcedure<I>(string query, I input, string output = "", bool forJob = false)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> ExecuteStoredProcedure<I, O>(string procedureName, I input, string output = null) where O : class
        {
            throw new NotImplementedException();
        }

        public Task<bool?> ExecuteStoredProcedureWithBooleanResult<I>(string procedureName, I input)
        {
            throw new NotImplementedException();
        }
    }
}
