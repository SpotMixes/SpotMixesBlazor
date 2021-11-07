using System.Collections.Generic;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Shared.ModelsLookup
{
    public class AudioLookup : Audio
    {
        public List<User> User { get; set; }        
    }
}