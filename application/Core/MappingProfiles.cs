using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.Activities.Commands;
using application.Activities.DTOs;
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

            
        }
    }
}