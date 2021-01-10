using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace api.Entities
{
    public class Activity
    {
        [Key] public string Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Price { get; set; }
        public string Description { get; set; }
        [Required] public string Address { get; set; }
        public string OpenHoursId { get; set; }
        public OpenHours OpenHours { get; set; }
        [JsonIgnore] public List<Booking> Bookings { get; set; }
        public Activity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}