using System;
using application.Core;
using application.Interfaces;
using Domain;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands;

public class FollowToggle
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string FolloweeUserId { get; set; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var follower = await userAccessor.GetUserAsync();
            var followee = await context.Users.FindAsync([request.FolloweeUserId], cancellationToken);
            if (followee == null) return Result<Unit>.Failure("Followee user not found", 400);
            var following = await context.UserFollowings.FindAsync([follower.Id, followee.Id], cancellationToken);

            if (following == null)
                context.UserFollowings.Add(new UserFollowing
                {
                    FollowerId = follower.Id,
                    FolloweeId = followee.Id,
                });

            else context.UserFollowings.Remove(following);

            return await context.SaveChangesAsync(cancellationToken) > 0
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Problem updating following", 400);
        }
    }
}
