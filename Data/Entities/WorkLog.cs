using System;

namespace Data.Entities
{
    public class WorkLog : BaseEntity
    {
        public int Hours { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int TaskId { get; set; }

        public virtual Task Task { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
