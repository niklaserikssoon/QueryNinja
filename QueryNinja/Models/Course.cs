using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    internal class Course
    {
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }
        public object CourseID { get; internal set; }
        public object Title { get; internal set; }
    }
}
