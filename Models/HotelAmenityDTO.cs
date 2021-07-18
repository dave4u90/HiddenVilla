﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HotelAmenityDTO
    {
        public HotelAmenityDTO()
        {
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter amenity name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter amenity timing")]
        public string Timing { get; set; }
        [Required(ErrorMessage = "Please enter amenity description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter amenity icon from font awesome")]
        public string IconStyle { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
