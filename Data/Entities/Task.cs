using System.Collections.Generic;
using System.ComponentModel;

namespace Data.Entities
{
    public class Task : BaseEntity
    {
        public Task()
        {
            this.WorkLogs = new HashSet<WorkLog>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayName("Assignee: ")]
        public int? AssigneeId { get; set; }

        public virtual User Assignee { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<WorkLog> WorkLogs { get; set; }
    }
}
