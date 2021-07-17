using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    public class HotelRoomImage
    {
        public HotelRoomImage()
        {
        }

        public int Id { get; set; }
        public int RoomID { get; set; }
        public string RoomImageUrl { get; set; }

        [ForeignKey("RoomID")]
        public virtual HotelRoom HotelRoom { get; set; }
    }
}
