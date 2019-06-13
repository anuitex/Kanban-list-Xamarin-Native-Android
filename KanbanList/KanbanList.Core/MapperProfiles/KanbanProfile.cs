using AutoMapper;
using KanbanList.Core.Entities;
using KanbanList.Core.Helpers;
using KanbanList.Core.Models;

namespace KanbanList.Core.MapperProfiles
{
    public class KanbanProfile : Profile
    {
        public KanbanProfile()
        {
            CreateMap<ImageModelEntity, ImageFileModelResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.ImageArray, opt => opt.MapFrom(src => PhotoHelper.ConvertToByteArrayFromPath(src.FilePath)));

            CreateMap<ImageFileModelResult, ImageModelEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath));

            CreateMap<TaskModelEntity, TaskModelResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedUserId))
                .ForMember(dest => dest.CreatorUserId, opt => opt.MapFrom(src => src.CreatorUserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
                .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.TaskStatus))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.LastEditDate, opt => opt.MapFrom(src => src.LastEditDate))
                .ForMember(dest => dest.ViewedDate, opt => opt.MapFrom(src => src.ViewedDate));

            CreateMap<TaskModelResult, TaskModelEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedUserId))
                .ForMember(dest => dest.CreatorUserId, opt => opt.MapFrom(src => src.CreatorUserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
                .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.TaskStatus))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.LastEditDate, opt => opt.MapFrom(src => src.LastEditDate))
                .ForMember(dest => dest.ViewedDate, opt => opt.MapFrom(src => src.ViewedDate));

            CreateMap<UserModelEntity, UserModelResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.OrganizationName));

            CreateMap<UserModelResult, UserModelEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.OrganizationName));
        }
    }
}
