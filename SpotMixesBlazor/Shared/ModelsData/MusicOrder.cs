using System;

namespace SpotMixesBlazor.Shared.ModelsData
{
    public class MusicOrder
    {
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}