using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeRound1.Models
{
    public class DepartmentCourseCountVM
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int? CourseCount { get; set; }
    }
}
