using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SpotMixesBlazor.Shared
{
    public class Audio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Genres { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string UrlAudio { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string UrlCover { get; set; }

        public string UserNickname { get; set; }
        
        public string UserUrlProfilePicture { get; set; }
        
        public DateTime DatePublication { get; set; } = DateTime.Now;
        public int NumReproduction { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}