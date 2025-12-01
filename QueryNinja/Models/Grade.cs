using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    public class Grade
    {
        [Key]
        public int GradeID { get; set; }

        public string GradeValue { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

    }
}
