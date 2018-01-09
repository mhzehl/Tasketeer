using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasketeer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Prefix { get; set; }
        public string Lastname { get; set; }
    }
}
