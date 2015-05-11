using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class Student : Person
    {
        public int RollNo { get; set; }
        public string Program { get; set; }
    }
}