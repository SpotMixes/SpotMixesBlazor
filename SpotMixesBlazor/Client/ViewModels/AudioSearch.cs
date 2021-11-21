using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class AudioSearch
    {
        [Required(ErrorMessage = "Campo requerido")]
        public string TextSearch { get; set; }
    }
}