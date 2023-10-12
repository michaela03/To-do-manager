using System.Collections.Generic;
using Data.Entities;

namespace ToDoManager.Models.Tasks
{
    public class IndexVM
    {
        public List<Task> Items { get; set; }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
