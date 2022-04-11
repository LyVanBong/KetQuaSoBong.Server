using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TrucTiepKetQua.net.Server.Configurations;
using TrucTiepKetQua.net.Shared.Models;

namespace TrucTiepKetQua.net.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private IMongoCollection<KqxsMbModel> _collectionMb;
        private IMongoCollection<KqxsMnModel> _collectionMn;
        private IMongoCollection<KqxsMnModel> _collectionMt;

        private string[] _region = new string[]
        {
            "northern","central","south"
        };
        public LotteryController()
        {
            MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
            IMongoDatabase database = mongo.GetDatabase("Kqxs");
            _collectionMb = database.GetCollection<KqxsMbModel>("KqxsMb");
            _collectionMn = database.GetCollection<KqxsMnModel>("KqxsMn");
            _collectionMt = database.GetCollection<KqxsMnModel>("KqxsMt");
        }

        /// <summary>
        /// Lấy kết quả xổ số 3 miền theo ngày
        /// </summary>
        /// <param name="region">Miền cần lấy
        /// "northern","central","south"
        /// </param>
        /// <param name="date">Ngày cần lấy kết quả
        /// d-m-yyyy
        /// </param>
        /// <returns>
        /// Kết Quả ngày hôm đó
        /// </returns>
        [HttpGet("{region}/{date}")]
        public IActionResult Get(string region, string date)
        {
            if (!string.IsNullOrEmpty(region) && !string.IsNullOrEmpty(date))
            {
                if (_region.Contains(region))
                {
                    if (DateTime.TryParse(date, new CultureInfo("vi-VN"), DateTimeStyles.None, out DateTime result))
                    {
                        if (region == _region[0])
                        {
                            var data = _collectionMb.Find(x => x.NgayQuay == date).First();
                            if (data != null)
                            {
                                return Ok(data);
                            }
                        }
                        else if (region == _region[1])
                        {
                            var data = _collectionMt.Find(x => x.NgayQuay == date).First();
                            if (data != null)
                            {
                                return Ok(data);
                            }
                        }
                        else if (region == _region[2])
                        {
                            var data = _collectionMn.Find(x => x.NgayQuay == date).First();
                            if (data != null)
                            {
                                return Ok(data);
                            }
                        }
                    }
                }
            }

            return BadRequest();
        }
    }
}
