using Configurations;
using Microsoft.AspNetCore.Mvc;
using Models.Supports;
using MongoDB.Driver;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly IMongoCollection<Support> _collectionSupport;

        public SupportController()
        {
            MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
            IMongoDatabase database = mongo.GetDatabase("Kqxs");
            _collectionSupport = database.GetCollection<Support>(nameof(Support));
        }

        [HttpPost("Post/{userName}/{numberPhone}/{email}")]
        public async Task<IActionResult> Post(string userName, string numberPhone, string email, [FromQuery] string comment)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(numberPhone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(comment))
            {
                return BadRequest(0);
            }

            await _collectionSupport.InsertOneAsync(new Support()
            {
                UserName = userName,
                NumberPhone = numberPhone,
                Email = email,
                Comment = comment,
            });
            return Ok(1);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var data = _collectionSupport.Find(Builders<Support>.Filter.Empty);
            if (data.Any())
            {
                var result = data.ToList();
                return Ok(result);
            }

            return BadRequest(0);
        }
    }
}
