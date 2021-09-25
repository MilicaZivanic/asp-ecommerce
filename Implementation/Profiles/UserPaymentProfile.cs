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
    public class UserPaymentProfile : Profile
    {
        public UserPaymentProfile()
        {
            CreateMap<UserPayment, UserPaymentDto>();
            CreateMap<UserPaymentDto, UserPayment>();
        }
    }
}
