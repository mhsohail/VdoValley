using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class Teacher : Person
    {
        public string Subject { get; set; }
        public DateTime EmployementDate { get; set; }
    }
}