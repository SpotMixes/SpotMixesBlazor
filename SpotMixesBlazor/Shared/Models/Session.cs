using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.Models
{
    public class Session
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string UserEmail { get; set; }
        
        public string UserJwt { get; set; }

        public SessionLocation SessionLocation { get; set; }

        public DateTime SessionDate{ get; set; } = DateTime.Now; 
    }
}