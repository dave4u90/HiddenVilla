using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public class HotelAmenity : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Timing { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter amenity icon from font awesome")]
        public string IconStyle { get; set; }
    }
}
