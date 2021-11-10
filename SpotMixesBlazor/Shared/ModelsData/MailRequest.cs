using System.Collections.Generic;

namespace SpotMixesBlazor.Shared.ModelsData
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}