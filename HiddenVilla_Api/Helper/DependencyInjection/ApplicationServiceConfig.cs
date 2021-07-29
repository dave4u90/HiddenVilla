using System;
using Business.Repository;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HiddenVilla_Api.Helper.DependencyInjection
{
    public static class ApplicationServiceConfig
    {
        public static IServiceCollection AddApplicationServiceSettings(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
            services.AddScoped<IHotelRoomImageRepository, HotelRoomImageRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
