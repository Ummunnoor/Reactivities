using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
// or replace with the actual namespace where AppDbContext is defined

namespace application.Activities.Queries
{
    public class GetActivityList
    {
        public class Query : IRequest<List<Domain.Activity>> { }
        public class Handler(AppDbContext context, ILogger<GetActivityList> logger) : IRequestHandler<Query, List<Domain.Activity>>
        {
            public async Task<List<Domain.Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(1000, cancellationToken);
                        logger.LogInformation($"Task {i} has completed");
                    }
                }
                catch (System.Exception)
                {

                    logger.LogInformation("Task has cancelled");
                }
                return await context.Activities.ToListAsync(cancellationToken);
            }
        }
    }
}