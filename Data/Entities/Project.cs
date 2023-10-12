using System.Collections.Generic;

namespace Data.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            this.Tasks = new HashSet<Task>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? LeadId { get; set; }

        public virtual User Lead { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
