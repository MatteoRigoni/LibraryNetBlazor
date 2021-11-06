using System.ComponentModel.DataAnnotations;

namespace Library.WebApp.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
