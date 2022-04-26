using System;
using System.Collections.Generic;

namespace MVCapp.DBModels
{
    public class Category
    {
        public Category()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string IconClassList { get; set; } = null!;

        public ICollection<Task> Tasks { get; set; }
    }
}
