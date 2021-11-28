using System.Collections.Generic;

namespace SpotMixesBlazor.Client.Helpers
{
    public class ListsHelper
    {
        public List<string> MusicGenres()
        {
            var list = new List<string>
            {
                "Cumbia",
                "Dance y EDM",
                "Dancehall",
                "Deep house",
                "Disco",
                "Dubstep",
                "Electrónica",
                "Hip hop y Rap",
                "House",
                "Jazz",
                "K-Pop",
                "Pop",
                "Rap",
                "Reggae",
                "Reggaetón",
                "Rock",
                "Salsa",
                "Techno",
                "Trap",
                "Urbano",
                "Vallenato"
            };
            
            return list;
        }
    }
}