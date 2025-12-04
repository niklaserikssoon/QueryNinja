using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [Required]
        [MaxLength(50)] 
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string AreaOfExpertise { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
