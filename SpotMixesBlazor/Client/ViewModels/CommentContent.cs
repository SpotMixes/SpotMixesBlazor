using System.ComponentModel.DataAnnotations;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class CommentContent
    {
        [Required(ErrorMessage = "⚠️ Campo requerido")]
        public string Content { get; set; }
    }
}