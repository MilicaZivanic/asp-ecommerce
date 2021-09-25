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
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ForMember(dest => dest.Category, o => o.MapFrom(src => src.Category.Name))
                                            .ForMember(dest => dest.Discount, o => o.MapFrom(src => src.Discount.Name));
            CreateMap<ProductDto, Product>().ForSourceMember(x => x.Id, o => o.DoNotValidate())
                                            .ForSourceMember(x => x.Category, o => o.DoNotValidate())
                                            .ForSourceMember(x => x.Discount, o => o.DoNotValidate());
        }
    }
}
