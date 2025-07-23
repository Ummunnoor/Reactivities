using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace application.Activities.DTOs
{
    public class EditActivityDto : BaseActivityDto
    {
        public string Id { get; set; } = "";
    }
}