using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
                var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
                await _db.SaveChangesAsync();
                return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                             _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_db.HotelRooms.AsNoTracking().Include(x => x.HotelRoomImages));

                
                if (!String.IsNullOrEmpty(checkOutDateStr) && !String.IsNullOrEmpty(checkInDateStr))
                {
                    foreach (var hotelRoomDTO in hotelRoomDTOs)
                    {
                        hotelRoomDTO.IsBooked = await IsRoomBooked(hotelRoomDTO.Id, checkInDateStr, checkOutDateStr);
                    }
                }
                
                return hotelRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                HotelRoomDTO hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.Include(x=> x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                if (!String.IsNullOrEmpty(checkOutDateStr) && !String.IsNullOrEmpty(checkInDateStr))
                {
                    hotelRoomDTO.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDateStr);
                }

                return hotelRoomDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            try
            {
                var roomDetails = await _db.HotelRooms.Include(x=> x.HotelRoomImages).FirstOrDefaultAsync(x=> x.Id == roomId);
                if(roomDetails != null)
                {
                    _db.HotelRoomImages.RemoveRange(roomDetails.HotelRoomImages);
                    _db.HotelRooms.Remove(roomDetails);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelRoomDTO hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.AsNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                    return hotelRoomDTO;
                }
                else
                {
                    HotelRoomDTO hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.AsNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != roomId));
                    return hotelRoomDTO;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (hotelRoomDTO.Id != 0)
                {
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(hotelRoomDTO.Id);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    // This causes all the relations and all the columns of the entity to get updated
                    // Need to fix bug by digging deep into change tracking
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public async Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                if(!String.IsNullOrEmpty(checkOutDateStr) && !String.IsNullOrEmpty(checkInDateStr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDateStr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDateStr, "MM/dd/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == roomId && x.IsPaymentSuccessful &&
                    //check if checkin date that user wants does not fall in between any dates for room that is booked
                    ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                    //check if checkout date that user wants does not fall in between any dates for room that is booked
                    || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                    )).FirstOrDefaultAsync();

                    if(existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
