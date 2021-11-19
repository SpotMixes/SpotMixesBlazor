using System.Threading.Tasks;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Services
{
    public class ReactionService
    {
        private readonly IMongoCollection<Reaction> _reactionCollection;
        
        public ReactionService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _reactionCollection = database.GetCollection<Reaction>("Reactions");
        }
        
        public async Task CreateReaction(Reaction reaction)
        {
            await _reactionCollection.InsertOneAsync(reaction);
        }
        
        public async Task<bool> DeleteReaction(string audioId, string userId)
        {
            var resultDelete = await _reactionCollection
                .DeleteOneAsync(reaction => reaction.AudioId == audioId && reaction.UserId == userId);

            return resultDelete.DeletedCount > 0;
        }
        
        public async Task<bool> IsReaction(string audioId, string userId)
        {
            var result = await _reactionCollection
                .FindAsync(reaction => reaction.AudioId == audioId && reaction.UserId == userId);

            return await result.AnyAsync();
        }

        public async Task<long> CountReactions(string audioId)
        {
            return await _reactionCollection.CountDocumentsAsync(r => r.AudioId == audioId);
        }
    }
}