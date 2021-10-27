using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using MongoDB.Bson;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using User = SpotMixesBlazor.Shared.User;

namespace SpotMixesBlazor.Server.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private const string ApiKey = "AIzaSyC21tr06NlSPv8GoI4Hkz4ZTIsr6xoOHFQ";
        private FirebaseAuthLink FirebaseEmpty { get; set; }


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

        public async Task<IReadOnlyList<User>> SearchUserByEmail(string email)
        {
            var user = await _usersCollection.Find(user => user.Email == email).ToListAsync();
            return user;
        }

        public async Task<IReadOnlyList<User>> SearchUserByUserName(string email)
        {
            var user = await _usersCollection.Find(user => user.Email == email).ToListAsync();
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
    }
}