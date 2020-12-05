using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class CreateRoleModel
    {    
        [Required]
        public string Name { get; set; }
    }

    public class DeleteRoleModel
    {
        [Required]
        public string Id { get; set; }
    }
}