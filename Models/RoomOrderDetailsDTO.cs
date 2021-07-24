﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class RoomOrderDetailsDTO
    {
        public RoomOrderDetailsDTO()
        {
        }

        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string StripeSessionId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }
        [Required]
        public long TotalCost { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public bool IsPaymentSuccessful { get; set; } = false;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public HotelRoomDTO HotelRoomDTO { get; set; }
        public string Status { get; set; }
    }
}