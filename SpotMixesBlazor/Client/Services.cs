namespace SpotMixesBlazor.Client
{
    public class PageService
    {
        public int ValueAll { get; set; } = 21;
        public bool ViewAll { get; set; } = true;

        public int ValueRecent { get; set; } = 0;
        public bool ViewRecent { get; set; } = true;

        public int ValueMostListened { get; set; } = 0;
        public bool ViewMostListened { get; set; } = true;
    }
    
    public class QuantityData
    {
        public string QuantityAudios { get; set; }
    }
}