using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            if(String.IsNullOrEmpty(checkInDate) || String.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Please enter both checkin date and checkout date",
                });
            }

            if(!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. Valid format is MM/dd/yyyy",
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOut date format. Valid format is MM/dd/yyyy",
                });
            }


            var allRooms = await _hotelRoomRepository.GetAllHotelRooms(checkInDate, checkOutDate);
            return Ok(allRooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
        {
            if (String.IsNullOrEmpty(checkInDate) || String.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Please enter both checkin date and checkout date",
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. Valid format is MM/dd/yyyy",
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOut date format. Valid format is MM/dd/yyyy",
                });
            }

            if (roomId == null)
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Missing resource ID",
                    ErrorMessage = "Invalid room ID",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkInDate, checkOutDate);

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
