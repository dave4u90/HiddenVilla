using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HotelRoomDTO
    {
        public HotelRoomDTO()
        {

        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter room name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter occupancy")]
        public int Occupancy { get; set; }
        [Range(1, 3000, ErrorMessage = "Regular rate must be between $1 to $3000")]
        [Required(ErrorMessage = "Please enter regulardate")]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        [Required(ErrorMessage = "Please enter size of the room")]
        public string SqFt { get; set; }

        public virtual ICollection<HotelRoomImageDTO> HotelRoomImages { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
