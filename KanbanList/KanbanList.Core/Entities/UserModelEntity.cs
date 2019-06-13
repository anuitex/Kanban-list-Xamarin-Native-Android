using KanbanList.Core.Entities.Base;
using SQLite;

namespace KanbanList.Core.Entities
{
    [Table("Users")]
    public class UserModelEntity : EntityBase
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Password { get; set; }

        public string OrganizationName { get; set; }
    }
}
