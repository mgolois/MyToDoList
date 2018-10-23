using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyToDoList.Models
{
    [Table("TodoItem")]
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int Priority { get; set; }
    }
}
