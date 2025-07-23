using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.Activities.Commands;
using application.Activities.DTOs;
using FluentValidation;

namespace application.Activities.validator
{
    public class EditActivityValidator : BaseActivityValidator<EditActivity.Command, EditActivityDto>
    {
        public EditActivityValidator() : base(x => x.ActivityDto)
        {
            RuleFor(x => x.ActivityDto.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}