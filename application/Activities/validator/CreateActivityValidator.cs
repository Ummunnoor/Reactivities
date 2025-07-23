using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.Activities.Commands;
using application.Activities.DTOs;
using FluentValidation;

namespace application.Activities.validator
{
    public class CreateActivityValidator : BaseActivityValidator<CreateActivity.Command, CreateActivityDto>
    {
        public CreateActivityValidator() : base(x => x.ActivityDto )
        {
            
        }
    }
}