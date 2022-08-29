using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}