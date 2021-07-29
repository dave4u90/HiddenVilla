using System;
using Business.Repository;
using Business.Repository.IRepository;
using HiddenVilla_Server.Service;
using HiddenVilla_Server.Service.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HiddenVilla_Server.Helper.DependencyInjection
{
    public static class ApplicationServiceConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.TryAddScoped<IHotelRoomRepository, HotelRoomRepository>();
            services.TryAddScoped<IHotelRoomImageRepository, HotelRoomImageRepository>();
            services.TryAddScoped<IAmenityRepository, AmenityRepository>();
            services.TryAddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();
            services.TryAddScoped<IDbInitializer, DbInitializer>();
            services.TryAddScoped<IFileUpload, FileUpload>();

            return services;
        }
    }
}
