using System.Collections.Generic;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Shared.ModelsLookup
{
    public class UserLookup : User
    {
        public List<Audio> Audios { get; set; }
    }
}