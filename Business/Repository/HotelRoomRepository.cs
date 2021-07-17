﻿using System;
using System.Collections.Generic;
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
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                             _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_db.HotelRooms.Include(x => x.HotelRoomImages));
                return hotelRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            try
            {
                HotelRoomDTO hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.Include(x=> x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));
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
                var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                if(roomDetails != null)
                {
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
                    await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                    return hotelRoomDTO;
                }
                else
                {
                    HotelRoomDTO hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != roomId));
                    return hotelRoomDTO;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
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
    }
}
