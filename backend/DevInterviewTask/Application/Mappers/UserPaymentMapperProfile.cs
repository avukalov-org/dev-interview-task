using AutoMapper;
using DevInterviewTask.Application.Commands.Payments;
using DevInterviewTask.Domain.Payments;

namespace DevInterviewTask.Application.Mappers
{
    public class UserPaymentMapperProfile : Profile
    {
        public UserPaymentMapperProfile()
        {
            CreateMap<UserPayment, UserPaymentEntity>().ReverseMap();
            CreateMap<AddPaymentCommand, UserPayment>();
        }
    }
}