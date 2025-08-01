using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.Activities.Commands;
using application.Activities.DTOs;
using application.Profiles.DTOs;
using AutoMapper;
using Domain;

namespace application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<EditActivityDto, Activity>();
            CreateMap<Activity, ActivityDto>()
             .ForMember(d => d.HostDisplayName, o => o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost)!.User.DisplayName))
             .ForMember(d => d.HostId, o => o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost)!.User.Id));
            CreateMap<ActivityAttendee, UserProfile>()
             .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.User.DisplayName))
             .ForMember(d => d.Bio, o => o.MapFrom(s => s.User.Bio))
             .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.User.ImageUrl))
             .ForMember(d => d.Id, o => o.MapFrom(s => s.User.Id));



            
        }
    }
}