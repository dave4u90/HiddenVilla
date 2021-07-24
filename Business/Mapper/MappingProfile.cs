using AutoMapper;
using DataAccess.Data;
using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoom, HotelRoomDTO>().ReverseMap();
            CreateMap<HotelRoomImageDTO, HotelRoomImage>().ReverseMap();
            CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();
            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ReverseMap();
        }
    }
}
