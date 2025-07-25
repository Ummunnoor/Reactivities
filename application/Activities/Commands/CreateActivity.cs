using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using application.Activities.DTOs;
using application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace application.Activities.Commands
{
    public class CreateActivity
    {
        public class Command : IRequest<Result<string>>
        {
            public required CreateActivityDto ActivityDto { get; set; }
        }
        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
        {
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = mapper.Map<Domain.Activity>(request.ActivityDto);
                context.Activities.Add(activity);
                var result = await context.SaveChangesAsync(cancellationToken) > 0;
                if (!result) return Result<String>.Failure("Failed to Create activity", 400);
                return Result<string>.Success(activity.Id);
            }
        }
    }
} 