using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class UserVerified
    {
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "⚠️ Solo se permiten números")]
        [MinLength(8, ErrorMessage = "⚠️ El DNI debe tener 8 dígitos")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [Range(18, 100, ErrorMessage = "⚠️ Edad no permitida")]
        public int Age { get; set; }
        
        public string FullName { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [EmailAddress(ErrorMessage = "⚠️ No es un email valido")]
        public string ContactEmail { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [Phone(ErrorMessage = "⚠️ No es un telefono valido")]
        [MinLength(8, ErrorMessage = "⚠️ El número debe tener 9 dígitos")]
        public string ContactMobile { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [RegularExpression("[a-zA-Z ]{2,254}", ErrorMessage = "⚠️ Solo se permite letras")]
        public string Department { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [RegularExpression("[a-zA-Z ]{2,254}", ErrorMessage = "⚠️ Solo se permite letras")]
        public string City { get; set; }
        
        public bool AvailableTime { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [Range(1, 1000, ErrorMessage = "⚠️ Monto no permitido")]
        public decimal Hourly { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [EmailAddress(ErrorMessage = "⚠️ No es un email valido")]
        public string EmailPayPal { get; set; }
        
        //[Required(ErrorMessage = "⚠️ Campo requerido")]
        public string FrontPhotoOfDni { get; set; }
        
        //[Required(ErrorMessage = "⚠️ Campo requerido")]
        public string ReversePhotoOfDni { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [StringLength(300, ErrorMessage = "⚠️ La descripción debe tener menos de 300 caracteres")]
        public string RequestDescription { get; set; }
    }
}