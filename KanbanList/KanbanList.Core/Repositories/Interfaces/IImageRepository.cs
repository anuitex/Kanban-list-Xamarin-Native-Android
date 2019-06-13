using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Base;

namespace KanbanList.Core.Repositories.Interfaces
{
    public interface IImageRepository<T> : IRepository<T> where T : class
    {
        Task DeleteAllImages(string taskId);
        Task<List<T>> GetAllImagesList(string taskId);
        Task<List<T>> GetAllImagesList();
    }
}
