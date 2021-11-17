using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;
using SpotMixesBlazor.Shared.ModelsLookup;

namespace SpotMixesBlazor.Server.Services
{
    public class AudioService
    {
        private readonly IMongoCollection<Audio> _audiosCollection;

        public AudioService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _audiosCollection = database.GetCollection<Audio>("Audios");
        }

        public async Task CreateAudio(Audio audio)
        {
            await _audiosCollection.InsertOneAsync(audio);
        }

        #region GetAudios
        public async Task<IReadOnlyList<AudioLookup>> GetAllAudios(int audioPerPage, int page)
        {
            var skip = audioPerPage * page;

            var audios = await _audiosCollection
                .Aggregate()
                .Lookup("Users", "UserId", "_id", "User")
                .Limit(audioPerPage)
                .Skip(skip)
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }
        
        public async Task<IReadOnlyList<AudioLookup>> GetRecentAudios(int audioPerPage, int page)
        {
            var skip = audioPerPage * page;
            
            var audios = await _audiosCollection
                .Aggregate()
                .SortByDescending(audio => audio.CreatedAt)
                .Lookup("Users", "UserId", "_id", "User")
                .Limit(audioPerPage)
                .Skip(skip)
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }
        
        public async Task<IReadOnlyList<AudioLookup>> GetMostListenedAudios(int audioPerPage, int page)
        {
            var skip = audioPerPage * page;
            
            var audios = await _audiosCollection
                .Aggregate()
                .SortByDescending(audio => audio.NumReproduction)
                .Lookup("Users", "UserId", "_id", "User")
                .Limit(audioPerPage)
                .Skip(skip)
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }
        
        public async Task<IReadOnlyList<AudioLookup>> SearchAudios(int audioPerPage, int page, string textSearch)
        {
            var skip = audioPerPage * page;

            var regularExpression = new BsonRegularExpression(textSearch, "/@/i");
            
            var audios = await _audiosCollection
                .Aggregate()
                .Match(Builders<Audio>.Filter.Regex(x => x.Title, regularExpression))
                .SortByDescending(audio => audio.NumReproduction)
                .Lookup("Users", "UserId", "_id", "User")
                .Limit(audioPerPage)
                .Skip(skip)
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }
        #endregion
    }
}