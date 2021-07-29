using System;
using Business.Repository;
using Business.Repository.IRepository;
using HiddenVilla_Server.Service;
using HiddenVilla_Server.Service.IService;
using Microsoft.Extensions.DependencyInjection;

namespace HiddenVilla_Server.Helper.DependencyInjection
{
    public static class ApplicationServiceConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
            services.AddScoped<IHotelRoomImageRepository, HotelRoomImageRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IFileUpload, FileUpload>();

            return services;
        }
    }
}
