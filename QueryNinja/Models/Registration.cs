using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    // Represents a registration entity linking students to courses with a registration date.
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int FkStudentId { get; set; }
        [ForeignKey("FkStudentId")]

        public Student Student { get; set; } = null!;

        public int FkCourseId { get; set; }
        [ForeignKey("FkCourseId")]

        public Course Course { get; set; } = null!;

    }
}
