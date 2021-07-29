using System;
using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HiddenVilla_Client.Helper.DependencyInjection
{
    public static class ApplicationServiceConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            services.AddScoped<IHotelRoomService, HotelRoomService>();
            services.AddScoped<IHotelAmenityService, HotelAmenityService>();
            services.AddScoped<IRoomOrderDetailsService, RoomOrderDetailsService>();
            services.AddScoped<IStripePaymentService, StripePaymentService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
