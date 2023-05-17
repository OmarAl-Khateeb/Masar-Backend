using API.Dtos;
using AutoMapper;
using Core.Entities;
using API.Extensions;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<AppUser, UserTokenDto>();
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()))
                .ForMember(d => d.PhotoUrl, o =>o.MapFrom(src => src.PhotoUrl))
            .ReverseMap();

            CreateMap<StudentCDto, Student>();
            CreateMap<Student, StudentDto>()
                .ForMember(d => d.StudentPhotoUrl, o => o.MapFrom(src => src.StudentPhotoUrl))
            .ReverseMap();

            CreateMap<TransactionCDto, Transaction>();
            CreateMap<Transaction, TransactionDto>()
                .ForMember(d => d.StudentId, o => o.MapFrom(src => src.StudentId))
                // .ForMember(d => d.DocumentUrl, o => o.MapFrom<ApiUrlResolver, string>(src => src.Document.DocumentUrl)) No longer need the resolver for cloudinary use
                .ForMember(d => d.DocumentUrl, o => o.MapFrom(src => src.Document.DocumentUrl))
            .ReverseMap();
        }
    }
}