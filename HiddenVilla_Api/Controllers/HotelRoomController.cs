using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelRooms()
        {
            var allRooms = await _hotelRoomRepository.GetAllHotelRooms();
            return Ok(allRooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId)
        {
            if(roomId == null)
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Missing resource ID",
                    ErrorMessage = "Invalid room ID",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value);

            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Room not found",
                    ErrorMessage = "Invalid room ID",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(roomDetails);
        }
    }
}
