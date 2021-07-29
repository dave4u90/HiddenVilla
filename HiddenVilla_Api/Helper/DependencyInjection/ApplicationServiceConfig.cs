using System;
using Business.Repository;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HiddenVilla_Api.Helper.DependencyInjection
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
            services.TryAddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
