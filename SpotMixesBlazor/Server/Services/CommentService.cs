using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;
using SpotMixesBlazor.Shared.ModelsLookup;

namespace SpotMixesBlazor.Server.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _commentsCollection;

        public CommentService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _commentsCollection = database.GetCollection<Comment>("Comments");
        }

        public async Task CreateComment(Comment comment)
        {
            await _commentsCollection.InsertOneAsync(comment);
        }

        public async Task<IReadOnlyList<CommentLookup>> GetAllComments(string audioId)
        {
            var comments = await _commentsCollection
                .Aggregate()
                .Match(Builders<Comment>.Filter.Eq(comment => comment.AudioId, audioId))
                .SortByDescending(comment => comment.CreatedAt)
                .Lookup("Users", "UserId", "_id", "User")
                .ToListAsync();

            var test = comments.Select(comment => BsonSerializer.Deserialize<CommentLookup>(comment)).ToList();

            return test;
        }
        
        public async Task<long> CountComments(string audioId)
        {
            return await _commentsCollection.CountDocumentsAsync(comment => comment.AudioId == audioId);
        }
    }
}