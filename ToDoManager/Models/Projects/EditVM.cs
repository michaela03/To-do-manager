using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoManager.Models.Projects
{
    public class EditVM
    {
        public int Id { get; set; }

        public int LeadId { get; set; }

        [DisplayName("Title: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Title { get; set; }

        [DisplayName("Description: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Description { get; set; }
    }
}
