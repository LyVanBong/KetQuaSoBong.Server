using Configurations;
using Microsoft.AspNetCore.Mvc;
using Models.BinhChon;
using MongoDB.Driver;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotedController : ControllerBase
    {
        private readonly IMongoCollection<BinhChon> _collectionBinhChon;
        private readonly IMongoCollection<LichSuBinhChon> _collectionLichSu;

        public VotedController()
        {
            MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
            IMongoDatabase database = mongo.GetDatabase("Kqxs");
            _collectionBinhChon = database.GetCollection<BinhChon>(nameof(BinhChon));
            _collectionLichSu = database.GetCollection<LichSuBinhChon>(nameof(LichSuBinhChon));
        }
        /// <summary>
        /// lấy danh sách bình chọn
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = _collectionBinhChon.Find(Builders<BinhChon>.Filter.Gt("SoLuongBinhChon", 0));
            if (data.Any())
            {
                return Ok(data.ToList());
            }

            return BadRequest(0);
        }

        [HttpPost("voted/{userName}")]
        public async Task<IActionResult> Post(string userName, [FromQuery] string nums)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(nums))
            {
                return BadRequest(0);
            }

            var num = nums.Split(',');
            if (num.Length > 3) return BadRequest(0);

            foreach (var s in num)
            {
                await _collectionLichSu.InsertOneAsync(new LichSuBinhChon()
                {
                    UserName = userName,
                    Num = s,
                });
            }
            TongHopBinhChon(num);
            return Ok(1);
        }

        private Task TongHopBinhChon(string[] num)
        {
            foreach (var s in num)
            {
                var count = _collectionLichSu.Find(Builders<LichSuBinhChon>.Filter.Eq("Num", s)).CountDocuments();
                var find = _collectionBinhChon.Find(Builders<BinhChon>.Filter.Eq("Number", s));
                if (find.Any())
                {
                    _collectionBinhChon.UpdateOneAsync(Builders<BinhChon>.Filter.Eq("Number", s), Builders<BinhChon>.Update.Set("SoLuongBinhChon", count));
                }
                else
                {
                    _collectionBinhChon.InsertOneAsync(new BinhChon()
                    {
                        Number = s,
                        SoLuongBinhChon = (int)count,
                    });
                }
            }
            return Task.CompletedTask;
        }
    }
}
