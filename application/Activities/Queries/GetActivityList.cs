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
        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Domain.Activity>>
        {
            public async Task<List<Domain.Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                
                return await context.Activities.ToListAsync(cancellationToken);
            }
        }
    }
}