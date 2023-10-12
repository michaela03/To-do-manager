using System.Collections.Generic;
using Data.Entities;

namespace ToDoManager.Models.Users
{
    public class IndexVM
    {
        public List<User> Items { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }
    }
}
