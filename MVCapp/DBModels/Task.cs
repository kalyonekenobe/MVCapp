using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCapp.DBModels
{
    public class Task
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please, enter the content of the task!")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "Please, choose the deadline of the task!")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Please, choose the category of the task!")]
        public int CategoryId { get; set; }
        public bool Expired { get; set; }
        public Category? Category { get; set; }
    }
}
