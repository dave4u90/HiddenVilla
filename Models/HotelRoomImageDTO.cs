using System;
namespace Models
{
    public class HotelRoomImageDTO
    {
        public HotelRoomImageDTO()
        {
        }

        public int Id { get; set; }
        public int RoomID { get; set; }
        public string RoomImageUrl { get; set; }
    }
}
