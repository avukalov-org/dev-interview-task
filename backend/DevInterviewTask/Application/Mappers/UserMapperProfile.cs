using AutoMapper;
using DevInterviewTask.Application.DTOs;
using DevInterviewTask.Domain.Users;

namespace DevInterviewTask.Application.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<ExternalUserDto, User>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserEntity>().ReverseMap();
            CreateMap<User, UserDto>();

        }
    }
}