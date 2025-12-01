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
        public int GradeId { get; set; }

        public string GradeValue { get; set; }

        [ForeignKey("FkTeacherId")]
        public int FkTeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [ForeignKey("FkCourseId")]
        public int FkCourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey("FkStudentId")]
        public int FkStudentId { get; set; }
        public Student Student { get; set; }

    }
}
