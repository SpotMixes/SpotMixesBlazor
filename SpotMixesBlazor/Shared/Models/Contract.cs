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

        public string DateEvent { get; set; }

        public string TimeEvent { get; set; }

        public string DurationEvent { get; set; }

        public string DescriptionEvent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}