﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasketeer.Models
{
    public class Todo
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
