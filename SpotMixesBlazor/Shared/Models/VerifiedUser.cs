using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.Models
{
    public class VerifiedUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        
        public string Dni { get; set; }

        public int Age { get; set; }
        
        public string FullName { get; set; }
        
        public string ContactEmail { get; set; }
        
        public string ContactMobile { get; set; }
        
        public string Department { get; set; }
        
        public string City { get; set; }

        public bool AvailableTime { get; set; }
        
        public decimal Hourly { get; set; }
        
        public string EmailPayPal { get; set; }
        
        public string FrontPhotoOfDni { get; set; }
        
        public string ReversePhotoOfDni { get; set; }
        
        public string RequestDescription { get; set; }
        
        private DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}