using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tarker.Booking.Application.Configuration;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;


namespace Tarker.Booking.Application
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapper.CreateMapper());
            services.AddTransient<ICreateUserCommand, CreateUserCommand>();

            return services;
        }
    }
}
