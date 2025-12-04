using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
