using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{

    public class ActivitiesController(AppDbContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Domain.Activity>>> GetActivities()
        {
            return await context.Activities.ToListAsync();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Domain.Activity>> GetActivityDetail(string Id)
        {
            var activity = await context.Activities.FindAsync(Id);
            if (activity == null) return NotFound();
            return activity;
            
        } 
    }
}