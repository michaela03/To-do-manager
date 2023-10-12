using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoManager.Models.Tasks
{
    public class CreateVM
    {
        public int ProjectId { get; set; }

        [DisplayName("Assignee: ")]
        public int AssigneeId { get; set; }

        public List<SelectListItem> UserPairs { get; set; }

        [DisplayName("Title: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Title { get; set; }

        [DisplayName("Description: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Description { get; set; }
    }
}
