using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNinja.Models
{
    // Represents a classroom entity with an ID and room number.
    public class ClassRoom
    {
        [Key]
        public int ClassRoomId { get; set; }

        [Required]
        public int RoomNumber { get; set; }
    
    }
}
