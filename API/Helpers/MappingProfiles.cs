using API.Dtos;
using AutoMapper;
using Core.Entities;
using API.Extensions;
using Core.Entities.Identity;
using Core.Entities.Meal;
using Core.Entities.Excercise;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<SubscriptionType, SubscriptionDto>().ReverseMap();
            
            CreateMap<Meal, MealDto>()
                .ReverseMap();

            CreateMap<MealPlanDto, MealPlan>()
                .ForMember(dest => dest.MealList, opt => opt.MapFrom(src => src.MealList))
                .ReverseMap();
            
            CreateMap<Excercise, ExcerciseDto>()
                .ReverseMap();
            
            CreateMap<ExcerciseSet, ExcerciseSetDto>()
                .ReverseMap();

            CreateMap<ExcercisePlanDto, ExcercisePlan>()
                .ForMember(dest => dest.Excerciselist, opt => opt.MapFrom(src => src.Excerciselist))
                .ReverseMap();
                
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()));
        }
    }
}