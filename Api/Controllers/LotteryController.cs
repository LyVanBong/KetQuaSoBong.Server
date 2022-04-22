using System.Globalization;
using Api.Helpers;
using Configurations;
using Microsoft.AspNetCore.Mvc;
using Models.Kqxs;
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
            if (string.IsNullOrEmpty(date)) return BadRequest(0);
            var filter = Builders<KqxsMbModel>.Filter.Eq("NgayQuay", date);
            var result = _collectionMb.Find(filter);
            if (result.Any())
            {
                return Ok(result.ToList());
            }
            var kq = await XoSoMienBac.GetData(@"https://xoso.me/xsmb-{0}-ket-qua-xo-so-mien-bac-ngay-{0}.html", date);
            if (kq != null)
            {
                var dt = DateTimeOffset.Parse(date, new CultureInfo("vi-VN"));
                if (dt.Date == DateTimeOffset.Now.Date)
                {
                    return Ok(kq);
                }
                await _collectionMb.InsertOneAsync(kq);
                return Ok(kq);
            }
            return BadRequest(0);
        }
        [HttpGet("central/{date}")]
        public async Task<IActionResult> GetCentral(string date)
        {
            if (string.IsNullOrEmpty(date)) return BadRequest(0);
            var filter = Builders<KqxsMnModel>.Filter.Eq("NgayQuay", date);
            var result = _collectionMt.Find(filter); 
            if (result.Any())
            {
                return Ok(result.ToList());
            }
            var kq = await XoSoMienNam.GetData(@"https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", date);
            if (kq != null)
            {
                var dt = DateTimeOffset.Parse(date, new CultureInfo("vi-VN"));
                if (dt.Date == DateTimeOffset.Now.Date)
                {
                    return Ok(kq);
                }
                await _collectionMt.InsertOneAsync(kq);
                return Ok(kq);
            }
            return BadRequest(0);
        }
        [HttpGet("south/{date}")]
        public async Task<IActionResult> GetSouth(string date)
        {
            if (string.IsNullOrEmpty(date)) return BadRequest(0);
            var filter = Builders<KqxsMnModel>.Filter.Eq("NgayQuay", date);
            var result = _collectionMn.Find(filter);
            if (result.Any())
            {
                return Ok(result.ToList());
            }
            var kq = await XoSoMienNam.GetData(@"https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", date);
            if (kq != null)
            {
                var dt = DateTimeOffset.Parse(date, new CultureInfo("vi-VN"));
                if (dt.Date == DateTimeOffset.Now.Date)
                {
                    return Ok(kq);
                }
                await _collectionMn.InsertOneAsync(kq);
                return Ok(kq);
            }
            return BadRequest(0);
        }
    }
}
