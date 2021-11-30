using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class ContractData
    {
        public string ContractedId { get; set; }
        
        public string ContractorId { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "⚠️ Solo se permiten números")]
        [MinLength(8, ErrorMessage = "⚠️ El DNI debe tener 8 dígitos")]
        public string Dni { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [Phone(ErrorMessage = "⚠️ No es un telefono valido")]
        [MinLength(8, ErrorMessage = "⚠️ El número debe tener 9 dígitos")]
        public string ContactMobile { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [EmailAddress(ErrorMessage = "⚠️ No es un email valido")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string DateEvent { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string TimeEvent { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [Range(1, 10, ErrorMessage = "⚠️ La duración del evento debe estar entre 1 y 10 horas")]
        public int DurationEvent { get; set; }
        
        public decimal CostPerHour { get; set; }

        public decimal CostTotal { get; set; }
        
        [StringLength(300, ErrorMessage = "⚠️ La descripción debe tener maximo 300 caracteres")]
        public string DescriptionEvent { get; set; }
    }
}