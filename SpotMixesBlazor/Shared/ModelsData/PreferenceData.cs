namespace SpotMixesBlazor.Shared.ModelsData
{
    public class PreferenceData
    {
        public string Title { get; set; }
        public string CurrencyId { get; set; } = "PEN";
        public string PictureUrl { get; set; } = "https://www.mercadopago.com/org-img/MP3/home/logomp3.gif";
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}