using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class AudioUpdateData
    {
        public string Id { get; set; }
        public string UrlCover { get; set; }
        
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        [StringLength(30, ErrorMessage = "⚠️ El título debe tener menos de 30 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string Genres { get; set; }

        [StringLength(300, ErrorMessage = "⚠️ La descripción debe tener menos de 300 caracteres")]
        public string Description { get; set; }
    }
}