using System;
using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Activity
    {
        [Key] 
        public string Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required] 
        public int Price { get; set; }
        public int MinTime { get; set; }
        [Required] 
        public int MaxTime { get; set; }
        public string Description { get; set; }

        public Activity()
        {
            Id = Guid.NewGuid().ToString();
            MinTime = 0;
            MaxTime = 120;
        }
    }
}