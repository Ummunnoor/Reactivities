using System;
using application.Core;
using application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries;

public class GetUserActivities
{
    public class Query : IRequest<Result<List<UserActivityDto>>>
    {
        public required string UserId { get; set; }
        public required string Filter { get; set; }
    }
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<UserActivityDto>>>
    {
        public async Task<Result<List<UserActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = context.ActivityAttendees
            .Where(x => x.User.Id == request.UserId)
            .OrderBy(x => x.Activity.Date)
            .Select(x => x.Activity)
            .AsQueryable();

            var today = DateTime.UtcNow;
            query = request.Filter switch
            {
                "past" => query.Where(a => a.Date <= today &&
                    a.Attendees.Any(u => u.UserId == request.UserId)),
                "isHosting" => query.Where(x =>
                    x.Attendees.Any(x => x.IsHost && x.UserId == request.UserId)),
                _ => query.Where(x => x.Date >= today &&
                     x.Attendees.Any(x => x.UserId == request.UserId))
            };
            var projectedActivities = query.ProjectTo<UserActivityDto>(mapper.ConfigurationProvider);
            var activities = await projectedActivities
            .ToListAsync(cancellationToken);
            return Result<List<UserActivityDto>>.Success(activities);
        }
    }
}
