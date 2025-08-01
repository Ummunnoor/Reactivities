using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.Core;
using application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace application.Activities.Commands
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required string Id { get; set; }
        }
        public class Handler(IUserAccessor userAccessor, AppDbContext context)
        : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities
                .Include(x => x.Attendees)
                .ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (activity == null) return Result<Unit>.Failure("No activity found", 404);
                var user = await userAccessor.GetUserAsync();
                var attendance = activity.Attendees.FirstOrDefault(x => x.UserId == user.Id);
                var isHost = activity.Attendees.Any(x => x.IsHost && x.UserId == user.Id);

                if (attendance != null)
                {
                    if (isHost) activity.IsCancelled = !activity.IsCancelled;
                    else activity.Attendees.Remove(attendance);
                }
                else
                {
                    activity.Attendees.Add(new ActivityAttendee
                    {
                        UserId = user.Id,
                        IsHost = false,
                        ActivityId = activity.Id
                    });

                }
                var result = await context.SaveChangesAsync(cancellationToken) > 0;
                return result
                ? Result<Unit>.Success(Unit.Value)
                : Result<Unit>.Failure("Problem updating the database", 400);
            }
        }
    }
}