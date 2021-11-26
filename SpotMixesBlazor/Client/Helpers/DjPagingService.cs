namespace SpotMixesBlazor.Client.Helpers
{
    public class DjPagingService
    {
        public int SkipVerified { get; set; } = 1;
        public bool FilterVerified { get; set; } = true;
        
        public int SkipRecent { get; set; } = 1;
        public bool FilterRecent { get; set; }
        
        public int SkipAll { get; set; } = 1;
        public bool FilterAll { get; set; }
        
        public long NumberOfUsersForPagination { get; set; }
    }
}