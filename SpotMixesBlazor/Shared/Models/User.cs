using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UrlProfile { get; set; }

        public string UrlProfilePicture { get; set; } =
            "https://res.cloudinary.com/kiulcode/image/upload/v1635369340/SpotMixes/UserData/ProfilePicture_kedk6f.svg";

        public string UrlCoverPicture { get; set; } =
            "https://res.cloudinary.com/kiulcode/image/upload/v1635287295/SpotMixes/UserData/CoverPhoto_vmzbci.svg";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Biography { get; set; }

        public bool IsDj { get; set; }

        public string CodeVerifyEmail { get; set; }

        public bool VerifiedEmail { get; set; } = false;
        
        public bool VerifiedProfile { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}