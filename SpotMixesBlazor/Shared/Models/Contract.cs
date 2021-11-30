using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.Models
{
    public class Contract
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ContractedId { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContractorId { get; set; }
        
        public string Dni { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public string City { get; set; }
        
        public string ContactMobile { get; set; }
        
        public string ContactEmail { get; set; }

        public string DateEvent { get; set; }

        public string TimeEvent { get; set; }

        public int DurationEvent { get; set; }
        
        public decimal CostPerHour { get; set; }

        public decimal CostTotal { get; set; }

        public string DescriptionEvent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}