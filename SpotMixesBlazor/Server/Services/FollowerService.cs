using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Services
{
    public class FollowerService
    {
        private IMongoCollection<Follower> _followersCollection;

        public FollowerService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _followersCollection = database.GetCollection<Follower>("Followers");
        }
        
        public async Task Follow(Follower follower)
        {
            await _followersCollection.InsertOneAsync(follower);
        }
        
        public async Task<bool> Unfollow(string followerId, string followedId)
        {
            try
            {
                var resultDelete = await _followersCollection
                    .DeleteOneAsync(follower => follower.FollowerId == followerId && follower.FollowedId == followedId);
            
                return resultDelete.DeletedCount == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> IsFollower(string followerId, string followedId)
        {
            var result = await _followersCollection
                .FindAsync(follower => follower.FollowerId == followerId && follower.FollowedId == followedId);
            
            return await result.AnyAsync();
        }

        public async Task<long> CountFollowers(string followedId)
        {
            return await _followersCollection.CountDocumentsAsync(follower => follower.FollowedId == followedId);
        }
    }
}