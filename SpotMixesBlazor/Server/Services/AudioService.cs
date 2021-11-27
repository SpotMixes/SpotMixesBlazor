using System;
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

        public async Task CreateAudio(Audio audio) => await _audiosCollection.InsertOneAsync(audio);

        public async Task<bool> DeleteAudio(string id)
        {
            try
            {
                var deleteResult = await _audiosCollection.DeleteOneAsync(audio => audio.Id == id);
                return deleteResult.DeletedCount == 1;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAudio(Audio audio)
        {
            try
            {
                var updateFilter = Builders<Audio>.Filter.Eq(a => a.Id, audio.Id);
            
                var updateDefinition = Builders<Audio>.Update
                    .Set(a => a.UrlCover, audio.UrlCover)
                    .Set(a => a.Title, audio.Title)
                    .Set(a => a.Genres, audio.Genres)
                    .Set(a => a.Description, audio.Description);

                var resultUpdate = await _audiosCollection
                    .UpdateOneAsync(updateFilter, updateDefinition);

                return resultUpdate.ModifiedCount == 1;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<long> CountAudios() => await _audiosCollection.CountDocumentsAsync(new BsonDocument());


        public async Task<IReadOnlyList<AudioLookup>> GetAllAudios(int audioPerPage, int page)
        {
            var audios = await _audiosCollection
                .Aggregate()
                .Skip((page - 1) * audioPerPage)
                .Limit(audioPerPage)
                .Lookup("Users", "UserId", "_id", "User")
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }

        public async Task<IReadOnlyList<AudioLookup>> GetRecentAudios(int audioPerPage, int page)
        {
            var audios = await _audiosCollection
                .Aggregate()
                .SortByDescending(audio => audio.CreatedAt)
                .Skip((page - 1) * audioPerPage)
                .Limit(audioPerPage)
                .Lookup("Users", "UserId", "_id", "User")
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }

        public async Task<IReadOnlyList<AudioLookup>> GetMostListenedAudios(int audioPerPage, int page)
        {
            var audios = await _audiosCollection
                .Aggregate()
                .SortByDescending(audio => audio.NumReproduction)
                .Skip((page - 1) * audioPerPage)
                .Limit(audioPerPage)
                .Lookup("Users", "UserId", "_id", "User")
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }

        public async Task<AudioLookup> GetAudioById(string audioId)
        {
            var audios = await _audiosCollection
                .Aggregate()
                .Match(Builders<Audio>.Filter.Eq(a => a.Id, audioId))
                .Lookup("Users", "UserId", "_id", "User")
                .FirstOrDefaultAsync();

            return BsonSerializer.Deserialize<AudioLookup>(audios);
        }

        public async Task<IReadOnlyList<AudioLookup>> GetAudioByGenre(int audioPerPage, int page, string textGenre)
        {
            var audios = await _audiosCollection
                .Aggregate()
                .Match(Builders<Audio>.Filter.Eq(audio => audio.Genres, textGenre))
                .SortByDescending(audio => audio.NumReproduction)
                .Skip((page - 1) * audioPerPage)
                .Limit(audioPerPage)
                .Lookup("Users", "UserId", "_id", "User")
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }
        
        public async Task<IReadOnlyList<AudioLookup>> SearchAudioByTitle(int audioPerPage, int page, string textSearch)
        {
            var regularExpression = new BsonRegularExpression(textSearch, "/@/i");

            var audios = await _audiosCollection
                .Aggregate()
                .Match(Builders<Audio>.Filter.Regex(audio => audio.Title, regularExpression))
                .SortByDescending(audio => audio.NumReproduction)
                .Skip((page - 1) * audioPerPage)
                .Limit(audioPerPage)
                .Lookup("Users", "UserId", "_id", "User")
                .ToListAsync();

            return audios.Select(audio => BsonSerializer.Deserialize<AudioLookup>(audio)).ToList();
        }
    }
}