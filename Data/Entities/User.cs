using System.Collections.Generic;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Projects = new HashSet<Project>();
            this.WorkLogs = new HashSet<WorkLog>();
            this.Tasks = new HashSet<Task>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<WorkLog> WorkLogs { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
