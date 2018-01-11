using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tasketeer.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }
        public User User { get; set; }
    }
}
