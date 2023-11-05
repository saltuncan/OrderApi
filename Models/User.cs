using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
