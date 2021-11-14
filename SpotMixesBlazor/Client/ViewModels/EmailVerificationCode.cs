using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class EmailVerificationCode
    {
        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(6, ErrorMessage = "El código de verificación debe tener 6 caracteres")]
        public string CodeVerifyEmail { get; set; }
    }
}