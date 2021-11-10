using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class UserRegister
    {
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "⚠️Campo requerido")]
        [EmailAddress(ErrorMessage = "No es un email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "⚠️ La contraseña debe contener mínimo 8 caracteres, una letra mayúscula y minúscula, un número y un carácter especial.o")]
        public string Password { get; set; }

        public bool IsDj { get; set; }
    }
}