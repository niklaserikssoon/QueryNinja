using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public int FkCourseId { get; set; }
        [ForeignKey("FkCourseId")]
        public Course Course { get; set; }

        public int FkClassRoomId { get; set; }
        [ForeignKey("FkClassRoomId")]
        public ClassRoom ClassRoom{ get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
