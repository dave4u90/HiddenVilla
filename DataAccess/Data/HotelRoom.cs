using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public class HotelRoom : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public double RegularRate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string SqFt { get; set; }

        public virtual ICollection<HotelRoomImage> HotelRoomImages { get; set; }
    }
}
