using KanbanList.Core.Models;
using System.Threading.Tasks;

namespace KanbanList.Core.Providers.Interfaces
{
    public interface ILoginProvider
    {
        Task<UserModelResult> Login(LoginModelResult loginModelResult);
    }
}
