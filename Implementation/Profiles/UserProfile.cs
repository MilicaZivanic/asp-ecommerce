using Application.Data_Transfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.Payments, o => o.MapFrom(src => src.UserPayments))
                                      .ForMember(dest => dest.Addresses, o => o.MapFrom(src => src.UserAddresses));
            CreateMap<UserDto, User>().ForSourceMember(x => x.Id, o => o.DoNotValidate());
        }
    }
}
