using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class UserRegister
    {
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "⚠️Campo requerido")]
        [EmailAddress(ErrorMessage = "No es un email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$",
            ErrorMessage =
                "⚠️ La contraseña debe contner mínimo 8 caracteres, al menos 1 letra mayúscula, una 1 minúscula y un número")]
        public string Password { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "⚠️ Debe aceptar para continuar.")]
        public bool PoliciesConditions { get; set; }
    }
}