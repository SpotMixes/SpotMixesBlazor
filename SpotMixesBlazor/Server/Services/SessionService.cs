using System.Threading.Tasks;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Services
{
    public class SessionService
    {
        private readonly IMongoCollection<Session> _sessionCollection;

        public SessionService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _sessionCollection = database.GetCollection<Session>("Sessions");
        }
        
        public async Task CreateSession(Session session)
        {
            await _sessionCollection.InsertOneAsync(session);
        }
        
        public async Task UpdateSession(Session session)
        {
            await _sessionCollection.ReplaceOneAsync(s => s.UserEmail == session.UserEmail, session);
        }
        
        public async Task<Session> GetSessionByUserEmail(string userEmail)
        {
            return await _sessionCollection.Find(session => session.UserEmail == userEmail).FirstOrDefaultAsync();
        }
    }
}