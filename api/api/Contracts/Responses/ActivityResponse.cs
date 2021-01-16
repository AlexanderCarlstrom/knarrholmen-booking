using System.Collections.Generic;
using api.DTOs;

namespace api.Contracts.Responses
{
    public class ActivityResponse : Response
    {
        public ActivityDto Activity { get; set; }
        public List<ActivityDto> Activities { get; set; }

        public ActivityResponse(int statusCode, ActivityDto activity) : base(true, statusCode)
        {
            Activity = activity;
        }
        
        public ActivityResponse(int statusCode, List<ActivityDto> activities) : base(true, statusCode)
        {
            Activities = activities;
        }

        public ActivityResponse(int statusCode, string message) : base(false, statusCode, message)
        {
        }
    }
}