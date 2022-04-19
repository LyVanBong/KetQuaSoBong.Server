using Api.Helpers;
using Api.Models;
using Configurations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private IMongoCollection<KqxsMbModel> _collectionMb;
        private IMongoCollection<KqxsMnModel> _collectionMn;
        private IMongoCollection<KqxsMnModel> _collectionMt;
        public LotteryController()
        {
            MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
            IMongoDatabase database = mongo.GetDatabase("Kqxs");
            _collectionMb = database.GetCollection<KqxsMbModel>("KqxsMb");
            _collectionMn = database.GetCollection<KqxsMnModel>("KqxsMn");
            _collectionMt = database.GetCollection<KqxsMnModel>("KqxsMt");
        }
        [HttpGet("northern/{date}")]
        public async Task<IActionResult> GetNorthern(string date)
        {
            if (string.IsNullOrEmpty(date)) return BadRequest();
            var filter = Builders<KqxsMbModel>.Filter.Eq("NgayQuay", date);
            var result = _collectionMb.Find(filter);
            if (result.Any())
            {
                return Ok(result.ToList());
            }
            var kq = await XoSoMienBac.GetData(@"https://xoso.me/xsmb-{0}-ket-qua-xo-so-mien-bac-ngay-{0}.html", date);
            if (kq != null)
            {
                await _collectionMb.InsertOneAsync(kq);
                return Ok(kq);
            }
            return BadRequest();
        }
        [HttpGet("central/{date}")]
        public async Task<IActionResult> GetCentral(string date)
        {
            if (string.IsNullOrEmpty(date)) return BadRequest();
            var filter = Builders<KqxsMnModel>.Filter.Eq("NgayQuay", date);
            var result = _collectionMt.Find(filter);
            if (result.Any())
            {
                return Ok(result.ToList());
            }
            var kq = await XoSoMienNam.GetData(@"https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", date);
            if (kq != null)
            {
                await _collectionMt.InsertOneAsync(kq);
                return Ok(kq);
            }
            return BadRequest();
        }
        [HttpGet("south/{date}")]
        public async Task<IActionResult> GetSouth(string date)
        {
            if (string.IsNullOrEmpty(date)) return BadRequest();
            var filter = Builders<KqxsMnModel>.Filter.Eq("NgayQuay", date);
            var result = _collectionMn.Find(filter);
            if (result.Any())
            {
                return Ok(result.ToList());
            }
            var kq = await XoSoMienNam.GetData(@"https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", date);
            if (kq != null)
            {
                await _collectionMn.InsertOneAsync(kq);
                return Ok(kq);
            }
            return BadRequest();
        }
    }
}
