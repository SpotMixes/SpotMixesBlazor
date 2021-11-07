/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Firebase.Auth;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared;
using User = SpotMixesBlazor.Shared.User;

namespace SpotMixesBlazor.Server.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private const string ApiKey = "AIzaSyC21tr06NlSPv8GoI4Hkz4ZTIsr6xoOHFQ";
        private FirebaseAuthLink FirebaseEmpty { get; set; }
        private User _user = new();


        public UserService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _usersCollection = database.GetCollection<User>("Users");
        }

        public async Task CreateUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            await _usersCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task<User> SearchUserByEmail(string email)
        {
            var userList = await _usersCollection.Find(user => user.Email == email).ToListAsync();
            foreach (var userData in userList)
            {
                _user = userData;
            }

            return _user;
        }

        public async Task<User> GetUserByUrlProfile(string urlProfile)
        {
            var userList = await _usersCollection.Find(user => user.UrlProfile == urlProfile).ToListAsync();
            
            User user = new();
            foreach (var userData in userList)
            {
                user = userData;
            }

            return user;
        }

        public async Task<FirebaseAuthLink> CreateUserWithEmailAndPassword(string email, string password)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var firebaseAuthLink = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
                return firebaseAuthLink;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<FirebaseAuthLink> SignInWithEmailAndPassword(string email, string password)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var firebaseAuthLink = await auth.SignInWithEmailAndPasswordAsync(email, password);
                return firebaseAuthLink;
            }
            catch (FirebaseAuthException e)
            {
                Console.WriteLine(e);
                return FirebaseEmpty;
            }
        }

        public async Task<User> GetAudiosByUrlProfile(string urlProfile)
        {
            var users = await _usersCollection
                .Aggregate()
                .Match(Builders<User>.Filter.Eq(u => u.UrlProfile, urlProfile))
                .Lookup("Audios", "_id", "UserId", "ListAudios")
                .ToListAsync();

            User user = new();
            foreach (var userData in users)
            {
                user = BsonSerializer.Deserialize<User>(userData);
            }
            
            return user;
        }
    }
}*/