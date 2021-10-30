using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared;

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

        public async Task<int> GetQuantityAudios()
        {
            var audios = await _audiosCollection.Find(audio => audio.IsActive == true).ToListAsync();
            return audios.Count;
        }

        public async Task<IReadOnlyList<Audio>> GetAllAudios(int audioPerPage, int page)
        {
            var skip = audioPerPage * page;
            var limit = audioPerPage;

            var audios = await _audiosCollection
                .Find(audio => audio.IsActive == true)
                .Limit(limit)
                .Skip(skip)
                .ToListAsync();
            
            return audios;
        }

        public async Task<IReadOnlyList<Audio>> GetRecentAudios(int audioPerPage, int page)
        {
            var skip = audioPerPage * page;
            var limit = audioPerPage;

            var audios = await _audiosCollection
                .Find(audio => audio.IsActive == true)
                .SortByDescending(audio => audio.DatePublication)
                .Limit(limit)
                .Skip(skip)
                .ToListAsync();

            return audios;
        }

        public async Task<IReadOnlyList<Audio>> GetMostListenedAudios(int audioPerPage, int page)
        {
            var skip = audioPerPage * page;
            var limit = audioPerPage;

            var audios = await _audiosCollection
                .Find(audio => audio.IsActive == true)
                .SortByDescending(audio => audio.NumReproduction)
                .Limit(limit)
                .Skip(skip)
                .ToListAsync();

            return audios;
        }

        public async Task<IReadOnlyList<Audio>> GetAudiosByGenre(int audioPerPage, int page, string genre)
        {
            var skip = audioPerPage * page;
            var limit = audioPerPage;

            var filter = @"{$and: [
                                {Genres: '" + genre + @"'},
                                {IsActive: true}
                            ]}";

            var audios = await _audiosCollection
                .Find(filter)
                .SortByDescending(audio => audio.NumReproduction)
                .Limit(limit)
                .Skip(skip)
                .ToListAsync();

            return audios;
        }

        public async Task<IReadOnlyList<Audio>> SearchAudios(int audioPerPage, int page, string textSearch)
        {
            var skip = audioPerPage * page;
            var limit = audioPerPage;
            var filter = @"{$and: [
                                    {Title: /" + textSearch + @"/i},
                                    {IsActive: true}
                                ]}";

            var audios = await _audiosCollection
                .Find(filter)
                .SortByDescending(audio => audio.NumReproduction)
                .Limit(limit)
                .Skip(skip)
                .ToListAsync();

            return audios;
        }
    }
}