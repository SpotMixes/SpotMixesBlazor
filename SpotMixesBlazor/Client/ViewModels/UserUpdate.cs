using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class UserUpdate
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string DisplayName { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string UrlProfile { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string UrlProfilePicture { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string UrlCoverPicture { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [StringLength(300, ErrorMessage = "⚠️ La biography debe tener menos de 300 caracteres")]
        public string Biography { get; set; }
    }
}