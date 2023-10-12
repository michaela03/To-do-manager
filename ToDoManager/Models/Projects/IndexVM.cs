using System.Collections.Generic;
using Data.Entities;

namespace ToDoManager.Models.Projects
{
    public class IndexVM
    {
        public List<Project> Items { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount { get; set; }
    }
}
