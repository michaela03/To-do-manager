using System;

namespace ToDoManager.Models.WorkLogs
{
    public class CreateVM
    {
        public int Hours { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int TaskId { get; set; }
    }
}
