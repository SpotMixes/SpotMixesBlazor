using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.Models
{
    public class Audio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string UrlCover { get; set; } =
            "https://res.cloudinary.com/kiulcode/image/upload/v1635287746/SpotMixes/AudioData/CoverAudio_o6dbl8.svg";

        public string UrlAudio { get; set; }

        public string Title { get; set; }

        public string Genres { get; set; }

        public string Description { get; set; } = "Audio sin descripción";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int NumReproduction { get; set; } = 0;
    }
}