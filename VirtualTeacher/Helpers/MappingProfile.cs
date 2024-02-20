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
                //.ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator));

            CreateMap<CourseCreateViewModel, Course>()
                // Assuming direct mapping for most properties
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic))
                .ForMember(dest => dest.CourseTopic, opt => opt.MapFrom(src => new CourseTopic { Topic = src.CourseTopic }))
                //.ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                //src.Photo != null ? $"https://your-cloud-storage-url/courses/{src.Photo}" : null))
                 // Ignoring properties that do not map directly
                .ForMember(dest => dest.Lectures, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignoring Id as it's database generated
        }
    }

}
