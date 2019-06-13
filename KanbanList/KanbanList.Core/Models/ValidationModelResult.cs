using System.Collections.Generic;

namespace KanbanList.Core.Models
{
    public class ValidationModelResult
    {
        public bool IsValid { get; set; }

        public Dictionary<string, string> ErrorMessages { get; set; }
    }
}
