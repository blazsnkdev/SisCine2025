using Microsoft.EntityFrameworkCore;
using SistemaCineMVC.Data.Repo;
using SistemaCineMVC.Models;

namespace SistemaCineMVC.Data.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly BdCine2025Context _context;
        private readonly DbSet<T> _dbSet;
        public Repository(BdCine2025Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public Task<int> CountEntityAsync()
        {
            return _dbSet.CountAsync();
        }
    }
}
