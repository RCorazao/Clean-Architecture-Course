using AutoMapper;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Domain.Entities.User;


namespace Tarker.Booking.Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, CreateUserModel>().ReverseMap();
        }
    }
}
