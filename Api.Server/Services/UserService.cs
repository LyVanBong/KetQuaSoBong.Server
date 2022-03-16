using Api.Server.Configurations;
using Models;
using MongoDB.Driver;

namespace Api.Server.Services;

public class UserService : IUserService
{
    private IMongoClient _mongoClient;
    private IMongoDatabase _mongoDatabase;
    private IMongoCollection<UserModel> _mongoCollection;
    public UserService()
    {
        _mongoClient = new MongoClient(Configuration.ConnectionStringMongodb);
        _mongoDatabase = _mongoClient.GetDatabase(Configuration.DatabaseName);
        _mongoCollection = _mongoDatabase.GetCollection<UserModel>(nameof(UserModel));
    }

    public async Task<List<UserModel>> GetAllUser() => await _mongoCollection.Find(_ => true).ToListAsync();

    public async Task<UserModel> GetUser(string username, string passwd) =>
        await _mongoCollection.Find(x => x.UserName == username && x.Password == passwd).FirstOrDefaultAsync();

    public async Task UpdateUser(UserModel user) =>
        await _mongoCollection.ReplaceOneAsync(x => x.UserName == user.UserName, user);

    public async Task DeleteUser(string userName) => await _mongoCollection.DeleteOneAsync(x => x.UserName == userName);

    public async Task AddUser(UserModel user) => await _mongoCollection.InsertOneAsync(user);
}