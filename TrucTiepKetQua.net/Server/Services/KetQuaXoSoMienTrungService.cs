using System.Globalization;
using MongoDB.Driver;
using TrucTiepKetQua.net.Server.Configurations;
using TrucTiepKetQua.net.Server.Helpers;
using TrucTiepKetQua.net.Server.Models;

namespace TrucTiepKetQua.net.Server.Services;

public class KetQuaXoSoMienTrungService : IHostedService, IDisposable
{
    private readonly ILogger<KetQuaXoSoMienNamService> _logger;
    private Timer _timer = null!;
    private DateTime _date;
    private string _url = @"https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html";
    private IMongoCollection<KqxsMnModel> _mongoCollection;
    private DateTime _timeStart = new DateTime(2022, 01, 01, 17, 10, 0);
    private DateTime _timeStop = new DateTime(2022, 01, 01, 17, 35, 0);
    private string _timeCheck = "nan";
    public KetQuaXoSoMienTrungService(ILogger<KetQuaXoSoMienNamService> logger)
    {
        _logger = logger;
        MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
        IMongoDatabase database = mongo.GetDatabase("Kqxs");
        _mongoCollection = database.GetCollection<KqxsMnModel>("KqxsMt");
        var sort = Builders<KqxsMbModel>.Sort.Ascending(nameof(KqxsMnModel.Id));
        var item = _mongoCollection.Find(x => x.Id != null).ToList().LastOrDefault();
        if (item != null)
        {
            var ngay = item.NgayQuay;
            if (string.IsNullOrEmpty(ngay))
            {
                _date = DateTime.Now;
            }
            else
            {
                _date = DateTime.Parse(ngay, new CultureInfo("vi-VN")).AddDays(1);
            }
        }
        else
        {
            _date = DateTime.Now;
        }
    }
    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        DateTime now = DateTime.Now;
        if (_date.Date < now.Date)
        {
            var kq = await XoSoMienNam.GetData(_url, _date, _logger);
            if (kq != null)
            {
                await _mongoCollection.InsertOneAsync(kq);
                _date = _date.AddDays(1);
            }
        }
        else if (_date.Date == now.Date)
        {
            if (now.TimeOfDay > _timeStart.TimeOfDay && now.TimeOfDay < _timeStop.TimeOfDay)
            {
                _date = now;
                var kq = await XoSoMienNam.GetData(_url, _date, _logger);
                if (kq != null)
                {
                    if (kq.NgayQuay == _timeCheck)
                    {
                        await _mongoCollection.ReplaceOneAsync(x => x.NgayQuay == kq.NgayQuay, kq);
                    }
                    else
                    {
                        _timeCheck = kq.NgayQuay;
                        await _mongoCollection.InsertOneAsync(kq);
                    }
                }
            }
            else if (now.TimeOfDay > _timeStop.TimeOfDay)
            {
                _date = now.AddDays(1);
            }
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}