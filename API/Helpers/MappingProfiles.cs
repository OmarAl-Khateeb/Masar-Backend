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
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalcuateAge()))
                .ForMember(d => d.PhotoUrl, o =>o.MapFrom(s => s.PhotoUrl))
            .ReverseMap();

            CreateMap<StudentCDto, Student>();
            CreateMap<Student, StudentDto>()
                // .ForMember(d => d.StudentPhotoUrl, o => o.MapFrom(s => s.StudentPhotoUrl))
            .ReverseMap();

            // CreateMap<DocumentCDto, Student>();
            CreateMap<DocumentCDto, Document>();
            CreateMap<Document, DocumentDto>()
            .ReverseMap();

            CreateMap<NoteCDto, Note>();
            CreateMap<Note, NoteDto>()
            .ReverseMap();

            CreateMap<NotificationCDto, Notification>();
            CreateMap<Notification, NotificationDto>()
                .ForMember(d => d.StudentId, o => o.MapFrom(s => s.StudentId))
                .ForMember(d => d.DocumentUrl, o => o.MapFrom(s => s.Document.DocumentUrl))
                .ForMember(d => d.RollerName, o => o.MapFrom(s => s.User.UserName))
            .ReverseMap();

            CreateMap<ActivityCDto, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.StudentId, o => o.MapFrom(s => s.StudentId))
                // .ForMember(d => d.Documents, o => o.MapFrom(s => s.Documents))
            .ReverseMap();

            CreateMap<TransactionTypeCDto, TransactionType>();
            CreateMap<TransactionType, TransactionTypeDto>()
            .ReverseMap();
            CreateMap<TransactionCDto, Transaction>();
            CreateMap<Transaction, TransactionDto>()
                .ForMember(d => d.StudentId, o => o.MapFrom(s => s.StudentId))
                // .ForMember(d => d.DocumentUrl, o => o.MapFrom<ApiUrlResolver, string>(s => s.Document.DocumentUrl)) No longer need the resolver for cloudinary use
                .ForMember(d => d.DocumentUrl, o => o.MapFrom(s => s.Document.DocumentUrl))
            .ReverseMap();
        }
    }
}