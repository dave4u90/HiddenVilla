using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public AmenityRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenity)
        {
            var amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenity);
            amenity.CreatedBy = "";
            amenity.CreatedDate = DateTime.UtcNow;
            var addedHotelAmenity = await _db.HotelAmenities.AddAsync(amenity);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addedHotelAmenity.Entity);
        }

        public async Task<int> DeleteHotelAmenity(int amenityId, string userId)
        {
            var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
            if (amenityDetails != null)
            {
                _db.HotelAmenities.Remove(amenityDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenity()
        {
            return _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(await _db.HotelAmenities.ToListAsync());
        }

        public async Task<HotelAmenityDTO> GetHotelAmenity(int amenityId)
        {
            var amenityData = await _db.HotelAmenities
                .FirstOrDefaultAsync(x => x.Id == amenityId);

            if (amenityData == null)
            {
                return null;
            }
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(amenityData);
        }

        public async Task<HotelAmenityDTO> IsSameNameAmenityAlreadyExists(string name)
        {
            try
            {
                var amenityDetails =
                    await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim()
                    );
                return _mapper.Map<HotelAmenity, HotelAmenityDTO>(amenityDetails);
            }
            catch (Exception ex)
            {

            }
            return new HotelAmenityDTO();
        }

        public async Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityId, HotelAmenityDTO hotelAmenity)
        {
            var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
            var amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenity, amenityDetails);
            amenity.UpdatedBy = "";
            amenity.UpdatedDate = DateTime.UtcNow;
            var updatedamenity = _db.HotelAmenities.Update(amenity);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedamenity.Entity);
        }
    }
}
