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
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, ReadOrderDto>().ForMember(dest => dest.AddressInfo, o => o.MapFrom(src => $"{src.Address}, {src.City} {src.Country} {src.PostalCode}"))
                                            .ForMember(dest => dest.UserInfo, o => o.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}, {src.User.Email} {src.User.Phone}"))
                                            .ForMember(dest => dest.PaymentInfo, o => o.MapFrom(src => src.AccountNumber))
                                            .ForMember(dest => dest.Status, o => o.MapFrom(src => src.OrderStatus.ToString()))
                                            .ForMember(dest => dest.OrderItems, o => o.MapFrom(src => src.OrderItems));
            CreateMap<ReadOrderDto, Order>();
        }
    }
}
