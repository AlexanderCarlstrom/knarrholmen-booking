using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace booking_api.Models
{
    public class Activity
    {
        [Key] public string Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Price { get; set; }

        /* Opening hours in 30 minutes interval
        eg 0 = 00:00, 1 = 00:30 */
        [Required]
        [Range(0, 48)]
        public int OpenTime { get; set; }

        [Required]
        [Range(0, 48)]
        public int CloseTime { get; set; }
        [Required] 
        public int MinMinutes { get; set; }
        [Required] 
        public int MaxMinutes { get; set; }
        public string Description { get; set; }

        public Activity()
        {
            Id = Guid.NewGuid().ToString();
            OpenTime = 0;
            CloseTime = 24;
            MinMinutes = 0;
            MaxMinutes = 120;
        }
    }
}