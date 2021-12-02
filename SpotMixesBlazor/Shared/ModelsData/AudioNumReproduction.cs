using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotMixesBlazor.Shared.ModelsData
{
    public class AudioNumReproduction
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}