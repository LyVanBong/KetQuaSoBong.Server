using Configurations;
using Microsoft.AspNetCore.Mvc;
using Models.Chats;
using MongoDB.Driver;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private int _lengMessage = 100;
        private IMongoCollection<ChatXoSo> _collectionXs;
        private IMongoCollection<ChatBongDa> _collectionBd;
        public ChatsController()
        {
            MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
            IMongoDatabase database = mongo.GetDatabase("Kqxs");
            _collectionXs = database.GetCollection<ChatXoSo>(nameof(ChatXoSo));
            _collectionBd = database.GetCollection<ChatBongDa>(nameof(ChatBongDa));
        }
        [HttpGet("BongDa")]
        public async Task<IActionResult> GetBongDa()
        {
            var data = _collectionBd.Find(Builders<ChatBongDa>.Filter.Empty);
            if (data.Any())
            {
                var result = data.ToList();
                var res100 = result.Take(_lengMessage);
                return Ok(res100);
            }

            return BadRequest(0);
        }
        [HttpGet("XoSo")]
        public async Task<IActionResult> GetXoSo()
        {
            var data =  _collectionXs.Find(Builders<ChatXoSo>.Filter.Empty);
            if (data.Any())
            {
                var result = data.ToList();
                var res100 = result.Take(_lengMessage);
                return Ok(res100);
            }

            return BadRequest(0);
        }
        [HttpPost("XoSo/{userName}/{name}")]
        public async Task<IActionResult> PostXoSo(string userName, string name, [FromQuery] string message)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(message))
            {
                return BadRequest(0);
            }

            await _collectionXs.InsertOneAsync(new ChatXoSo()
            {
                Message = message,
                Name = name,
                UserName = userName,
            });
            return Ok(1);
        }
        [HttpPost("BongDa/{userName}/{name}")]
        public async Task<IActionResult> PostBongDa(string userName, string name, [FromQuery] string message)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(message))
            {
                return BadRequest(0);
            }

            await _collectionBd.InsertOneAsync(new ChatBongDa()
            {
                Message = message,
                Name = name,
                UserName = userName,
            });
            return Ok(1);
        }
    }
}
