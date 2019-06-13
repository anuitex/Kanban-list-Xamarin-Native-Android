using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Base;

namespace KanbanList.Core.Repositories.Interfaces
{
    public interface ITaskRepository<T> : IRepository<T> where T : class
    {
        Task<List<T>> GetAllTasksList();
        Task<List<T>> GetAllTasksList(string userId);
    }
}
