using System.ComponentModel.DataAnnotations;
namespace Bloggie.web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)] // Extra: Hides input characters
        public string Password { get; set; }
        

    }
}
