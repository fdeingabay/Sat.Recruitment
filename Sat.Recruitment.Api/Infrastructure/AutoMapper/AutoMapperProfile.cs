using AutoMapper;
using Domain.DTOs;
using Sat.Recruitment.Api.Requests;

namespace Sat.Recruitment.Api.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserRequest, UserDto>();
        }
    }
}