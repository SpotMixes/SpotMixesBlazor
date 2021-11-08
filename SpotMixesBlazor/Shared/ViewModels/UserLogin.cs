using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Shared.ViewModels
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Campo requerido")]
        [EmailAddress(ErrorMessage = "No es un email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Password { get; set; }
    }
}