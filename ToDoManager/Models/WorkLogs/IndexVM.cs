using System.Collections.Generic;
using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoManager.Models.WorkLogs
{
    public class IndexVM
    {
        public List<WorkLog> Items { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }

        public int TaskId { get; set; }

        public Task Task { get; set; }

        public List<SelectListItem> UserPairs { get; set; }
    }
}
