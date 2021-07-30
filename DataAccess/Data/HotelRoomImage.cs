using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    public class HotelRoomImage : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public int RoomID { get; set; }
        [Required]
        public string RoomImageUrl { get; set; }

        [ForeignKey("RoomID")]
        public virtual HotelRoom HotelRoom { get; set; }
    }
}
