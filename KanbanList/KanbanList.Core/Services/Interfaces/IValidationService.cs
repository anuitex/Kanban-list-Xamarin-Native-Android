using KanbanList.Core.Models;

namespace KanbanList.Core.Services.Interfaces
{
    public interface IValidationService
    {
        ValidationModelResult ValidateModel(object model, bool validateAllProperties = true);
    }
}
