using AutoMapper;
using DevInterviewTask.Application.DTOs;
using DevInterviewTask.Domain.Users;

namespace DevInterviewTask.Application.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();
            CreateMap<User, UserDto>();

        }
    }
}