﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.Models
{
    public class Follower
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        //Seguidor
        [BsonRepresentation(BsonType.ObjectId)]
        public string FollowerId { get; set; }
        
        //Seguido
        [BsonRepresentation(BsonType.ObjectId)]
        public string FollowedId { get; set; }
        
        public DateTime FollowedOn { get; set; } = DateTime.Now;
    }
}