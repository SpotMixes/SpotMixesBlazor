using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared
{
    public class User
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }

        public string Nickname { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string UrlProfilePicture { get; set; } =
            "https://res.cloudinary.com/kiulcode/image/upload/v1635369340/SpotMixes/UserData/ProfilePicture_kedk6f.svg";

        public string UrlCoverPicture { get; set; } =
            "https://res.cloudinary.com/kiulcode/image/upload/v1635287295/SpotMixes/UserData/CoverPhoto_vmzbci.svg";

        public string Biography { get; set; }
        public bool IsDj { get; set; } = false;
        public bool PoliciesConditions { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}