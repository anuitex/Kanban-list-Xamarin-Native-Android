using KanbanList.Core.Models;
using System.Threading.Tasks;

namespace KanbanList.Core.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageFileModelResult> TakePhoto(bool saveToAlbum = true);

        Task<ImageFileModelResult> PickPhoto();
    }
}
