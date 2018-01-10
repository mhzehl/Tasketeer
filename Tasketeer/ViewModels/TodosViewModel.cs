﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tasketeer.ViewModels
{
    public class TodosViewModel
    {
        public IEnumerable<TodoDto> Todos { get; set; }
    }
    
    public class TodoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }
    }

    public class AddTodoViewModel
    {
        public int Id { get; set; }
        [DisplayName("Task")]
        //[Required(ErrorMessage = "Title is required")]
        public string Name { get; set; }
        [DisplayName("Task Description")]
        public string Description { get; set; }
        [DisplayName("Mark as completed")]
        public bool isCompleted { get; set; }
    }
}
