using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Services
{
    public class VerifiedUserService
    {
        private readonly IMongoCollection<VerifiedUser> _verifiedUsersCollection;

        public VerifiedUserService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _verifiedUsersCollection = database.GetCollection<VerifiedUser>("VerifiedUsers");
        }
        
        public async Task CreateVerifiedUser(VerifiedUser verifiedUser)
        {
            await _verifiedUsersCollection.InsertOneAsync(verifiedUser);
        }
        
        public async Task<VerifiedUser> GetVerifiedUserByUserId (string userId)
        {
            try
            {
                return await _verifiedUsersCollection
                    .Find(user => user.UserId == userId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}