using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace api.Entities
{
    public class OpenHours
    {
        [Key] 
        public string Id { get; set; }
        public int Open { get; set; }
        public int Close { get; set; }
        public string ActivityId { get; set; }
        public Activity Activity { get; set; }

        public OpenHours()
        {
            Id = Guid.NewGuid().ToString();
            Open = 0;
            Close = 48;
        }
    }
}