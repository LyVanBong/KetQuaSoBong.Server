using Configurations;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace CheckInService;

public class CheckInService : IHostedService, IDisposable
{
    private Timer _timer = null!;
    private bool _isDateTimeCheckIn = false;
    private bool _isDateTimeCheckOut = false;
    private DateTimeOffset _dateTimeCheckIn;
    private DateTimeOffset _dateTimeCheckOut;
    private List<CheckInModel> _userCheckIns;
    private IMongoCollection<CheckInModel> _collectionCheckIn;
    public CheckInService()
    {
        MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
        IMongoDatabase database = mongo.GetDatabase("Kqxs");
        _collectionCheckIn = database.GetCollection<CheckInModel>("CheckIn");
    }
    public async Task StartAsync(CancellationToken stoppingToken)
    {
        await TelegramHelper.SendMessage($"[{DateTime.Now}] Bắt đầu khởi động phầm mền check in out");
        var data = _collectionCheckIn.Find(Builders<CheckInModel>.Filter.Empty);
        if (data.Any())
        {
            _userCheckIns = data.ToList();
        }
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(2));
    }

    private async void DoWork(object? state)
    {
       
        if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday)
        {
            if (_userCheckIns != null && _userCheckIns.Any())
            {
                foreach (var user in _userCheckIns)
                {
                    _dateTimeCheckIn = DateTimeOffset.Parse(user.TimeStart);
                    _dateTimeCheckOut = DateTimeOffset.Parse(user.TimeEnd);
                    if (!_isDateTimeCheckOut && DateTimeOffset.Now.TimeOfDay > _dateTimeCheckOut.TimeOfDay && DateTimeOffset.Now.TimeOfDay < _dateTimeCheckOut.AddMinutes(15).TimeOfDay)
                    {
                        await TelegramHelper.SendMessage($"[{DateTime.Now}] [{user.UserName}] CheckIn");
                        _isDateTimeCheckOut = true;
                        _isDateTimeCheckIn = false;
                        await CheckInHelper.CheckInOut(user.UserName, user.Passwd);
                    }
                    else if (!_isDateTimeCheckIn && DateTimeOffset.Now.TimeOfDay > _dateTimeCheckIn.AddMinutes(-15).TimeOfDay && DateTimeOffset.Now.TimeOfDay < _dateTimeCheckIn.TimeOfDay)
                    {
                        await TelegramHelper.SendMessage($"[{DateTime.Now}] [{user.UserName}] CheckIn");
                        _isDateTimeCheckIn = true;
                        _isDateTimeCheckOut = false;
                        await CheckInHelper.CheckInOut(user.UserName, user.Passwd);
                    }
                }
            }
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}