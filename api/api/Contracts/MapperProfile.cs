using api.DTOs;
using api.Entities;
using AutoMapper;

namespace api.Contracts
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Activity, ActivityDto>();
            CreateMap<Booking, PublicBookingsDto>();
            CreateMap<Booking, PrivateBookingsDto>();
        }
    }
}