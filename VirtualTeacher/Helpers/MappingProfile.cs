using AutoMapper;
using VirtualTeacher.Models.ViewModel.CourseViewModel;
using VirtualTeacher.Models;
using System.Security.AccessControl;

namespace VirtualTeacher.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CourseTopic, opt => opt.MapFrom(src => src.CourseTopic))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Lectures, opt => opt.MapFrom(src => src.Lectures))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator));



        }
    }
}
