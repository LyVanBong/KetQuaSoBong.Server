using Configurations;
using Microsoft.AspNetCore.Mvc;
using Models.Users;
using MongoDB.Driver;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMongoCollection<UserModel> _collectionUser;
        public UserController()
        {
            MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
            IMongoDatabase database = mongo.GetDatabase("Kqxs");
            _collectionUser = database.GetCollection<UserModel>(nameof(UserModel));
        }
        [HttpPost("login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return BadRequest(0);
            var find = _collectionUser.Find(Builders<UserModel>.Filter.Eq("UserName", username));
            if (find != null && find.Any())
            {
                var user = find.First();
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Passwd))
                    return Ok(user);
            }
            return BadRequest(0);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] UserModel user)
        {
            if (user == null) return BadRequest(0);

            var find = _collectionUser.Find(Builders<UserModel>.Filter.Eq("UserName", user.UserName));
            if (find != null && find.Any()) return BadRequest(0);
            user.Passwd = BCrypt.Net.BCrypt.HashPassword(user.Passwd);
            await _collectionUser.InsertOneAsync(user);
            return Ok(1);
        }
    }
}
