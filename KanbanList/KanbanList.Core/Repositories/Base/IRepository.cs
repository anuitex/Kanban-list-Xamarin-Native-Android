using System.Threading.Tasks;

namespace KanbanList.Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task Create(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}
