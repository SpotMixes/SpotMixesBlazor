using System.Threading.Tasks;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Services
{
    public class PlaylistServices
    {
        private readonly IMongoCollection<Playlist> _playlistsCollection;
        
        public PlaylistServices(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _playlistsCollection = database.GetCollection<Playlist>("Playlist");
        }
        
        public async Task CreatePlaylist(Playlist playlist)
        {
            await _playlistsCollection.InsertOneAsync(playlist);
        }

        public async Task<bool> DeletePlaylist(string audioId, string userId)
        {
            var resultDelete = await _playlistsCollection
                .DeleteOneAsync(playlist => playlist.AudioId == audioId && playlist.UserId == userId);

            return resultDelete.DeletedCount > 0;
        }
        
        public async Task<bool> IsPlaylist(string audioId, string userId)
        {
            var result = await _playlistsCollection
                .FindAsync(playlist => playlist.AudioId == audioId && playlist.UserId == userId);

            return await result.AnyAsync();
        }
    }
}