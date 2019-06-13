using KanbanList.Core.Models;
using System.Threading.Tasks;

namespace KanbanList.Core.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<ValidationModelResult> Registrate(UserModelResult user);
    }
}
