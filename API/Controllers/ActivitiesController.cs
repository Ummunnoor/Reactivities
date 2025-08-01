
using application.Activities.Commands;
using application.Activities.DTOs;
using application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class ActivitiesController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ActivityDto>>> GetActivities()
        {
            return await Mediator.Send(new GetActivityList.Query());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ActivityDto>> GetActivityDetail(string id)
        {
            return HandleResult(await Mediator.Send(new GetActivityDetails.Query { Id = id }));


        }
        [HttpPost]
        public async Task<ActionResult<string>> CreateActivity([FromBody] CreateActivityDto activityDto)
        {
            return HandleResult(await Mediator.Send(new CreateActivity.Command { ActivityDto = activityDto }));
        }
        
        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<ActionResult> EditActivity(string id, EditActivityDto activity)
        {
            activity.Id = id;
            return HandleResult(await Mediator.Send(new EditActivity.Command { ActivityDto = activity }));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActivity(string id)
        {
            return HandleResult(await Mediator.Send(new DeleteActivity.Command { Id = id }));

        }

        [HttpPost("{id}/attend")]
        public async Task<ActionResult> Attend(string id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
        }
    }
}