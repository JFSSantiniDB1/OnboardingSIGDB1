using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;

namespace OnboardingSIGDB1.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SIGDB1DbContext _context;
        
        public UnitOfWork(SIGDB1DbContext context)
        {
            _context = context;
        }
        
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}