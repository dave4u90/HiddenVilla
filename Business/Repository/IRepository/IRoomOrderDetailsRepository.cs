using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IRoomOrderDetailsRepository
    {
        public Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details);
        public Task<RoomOrderDetailsDTO> MarkPaymentSucccessful(int id);
        public Task<RoomOrderDetailsDTO> GetRoomOrderDetail(int roomOrderId);
        public Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetails();
        public Task<bool> UpdateOrderStatus(int roomOrderId, string status);
        public Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate);
    }
}
