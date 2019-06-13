using MvvmCross.Commands;
using System;

namespace KanbanList.Core.Models
{
    public class ImageFileModelResult
    {
        public string Id { get; set; }

        public string TaskId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public byte[] ImageArray { get; set; }

        public Action<ImageFileModelResult> DeleteImage { get; set; }

        public IMvxCommand DeleteImageCommand => new MvxCommand(() =>
        {
            DeleteImage?.Invoke(this);
        });
    }
}
