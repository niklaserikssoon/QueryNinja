using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    // Represents a grade entity linking students, courses, and teachers.
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        public string GradeValue { get; set; }

        public int FkTeacherId { get; set; }
        [ForeignKey("FkTeacherId")]

        public Teacher Teacher { get; set; }  

        public int FkCourseId { get; set; }
        [ForeignKey("FkCourseId")]

        public Course Course { get; set; }

        public int FkStudentId { get; set; }
        [ForeignKey("FkStudentId")]

        public Student Student { get; set; }
    }
}
