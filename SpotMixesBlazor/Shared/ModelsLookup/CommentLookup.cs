using System.Collections.Generic;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Shared.ModelsLookup
{
    public class CommentLookup : Comment
    {
        public List<User> User { get; set; }
    }
}