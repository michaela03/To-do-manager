using System.Collections.Generic;

namespace ToDoManager.Models.Tasks
{
    public class MyTasksVM
    {
        public class MyTasks
        {
            public string ProjectTitle { get; set; }

            public string TaskTitle { get; set; }

            public int TaskId { get; set; }
        }

        public List<MyTasks> Items { get; set; }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }
    }
}
