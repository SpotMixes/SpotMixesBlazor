using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string AudioId { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string Content { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}