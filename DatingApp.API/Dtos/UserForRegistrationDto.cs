using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegistrationDto
    {
        [Required]
        public string username { get; set; }     
         [Required]
        public string Password { get; set; }     
    }
}