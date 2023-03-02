using API.Dtos;
using AutoMapper;
using Core.Entities;
using API.Extensions;
using Core.Entities.Identity;
using Core.Entities.Meal;
using Core.Entities.Excercise;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductCDto, Product>();
            
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
            
            CreateMap<SubscriptionType, SubscriptionDto>().ReverseMap();
            
            CreateMap<AdvertCDto, Advert>();
            CreateMap<Advert, AdvertDto>()
                .ReverseMap();
            
            CreateMap<NotificationCDto, Notification>();
            CreateMap<Notification, NotificationDto>()
                .ReverseMap();
            
            CreateMap<MealCDto, Meal>();
            CreateMap<Meal, MealDto>()
                .ReverseMap();

            CreateMap<MealCPlanDto, MealPlan>()
                .ForMember(dest => dest.MealList, opt => opt.MapFrom(src => src.MealList));
            CreateMap<MealPlanDto, MealPlan>()
                .ForMember(dest => dest.MealList, opt => opt.MapFrom(src => src.MealList))
                .ReverseMap();
            
            CreateMap<Excercise, ExcerciseDto>()
                .ReverseMap();
            
            CreateMap<ExcerciseCSetDto, ExcerciseSet>();
            CreateMap<ExcerciseSet, ExcerciseSetDto>()
                .ReverseMap();
            
            CreateMap<OrderCDto, Order>();
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>())
                .ReverseMap();

            CreateMap<ExcerciseCPlanDto, ExcercisePlan>()
                .ForMember(dest => dest.Excerciselist, opt => opt.MapFrom(src => src.Excerciselist));
            CreateMap<ExcercisePlanDto, ExcercisePlan>()
                .ForMember(dest => dest.Excerciselist, opt => opt.MapFrom(src => src.Excerciselist))
                .ReverseMap();

            CreateMap<AppUser, UserTokenDto>();
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()))
                .ReverseMap();
        }
    }
}