using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Base;

namespace KanbanList.Core.Repositories.Interfaces
{
    public interface IUserRepository<T> : IRepository<T> where T : class
    {
        Task<T> GetUser(string email);
        Task<T> GetUser(string email, string password);
        Task<T> GetUserById(string userId);
        Task<List<T>> GetAllUsersList();
    }
}
