namespace SpotMixesBlazor.Client.Helpers
{
    public class AudioPagingService
    {
        public int SkipMostListened { get; set; } = 1;
        public bool FilterMostListened { get; set; } = true;
        
        public int SkipRecent { get; set; } = 1;
        public bool FilterRecent { get; set; }
        
        public int SkipAll { get; set; } = 1;
        public bool FilterAll { get; set; }
    }
}