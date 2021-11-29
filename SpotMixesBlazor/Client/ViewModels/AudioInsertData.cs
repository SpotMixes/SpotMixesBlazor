using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class AudioInsertData
    {
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string UrlCover { get; set; } =
            "https://res.cloudinary.com/kiulcode/image/upload/v1635287746/SpotMixes/AudioData/CoverAudio_o6dbl8.svg";

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string UrlAudio { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [StringLength(30, ErrorMessage = "⚠️ El título debe tener menos de 30 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string Genres { get; set; }

        [StringLength(300, ErrorMessage = "⚠️ La descripción debe tener menos de 300 caracteres")]
        public string Description { get; set; }
    }
}