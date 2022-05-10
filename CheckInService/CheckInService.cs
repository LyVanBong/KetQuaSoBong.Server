using Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace CheckInService;

public class CheckInService : BackgroundService
{
    private Timer _timer = null!;
    private bool _isDateTimeCheckIn = false;
    private bool _isDateTimeCheckOut = false;
    private DateTimeOffset _dateTimeCheckIn;
    private DateTimeOffset _dateTimeCheckOut;
    private List<CheckInModel> _userCheckIns;
    private IMongoCollection<CheckInModel> _collectionCheckIn;
    private ILogger<CheckInService> _logger;
    public CheckInService(ILogger<CheckInService> logger)
    {
        _logger = logger;
        MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
        IMongoDatabase database = mongo.GetDatabase("CheckIn");
        _collectionCheckIn = database.GetCollection<CheckInModel>("Users");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Bắt đầu chạy service");
        await TelegramHelper.SentMessage($"[{DateTime.Now}] Bắt đầu khởi động phầm mền check in out");
        var data = _collectionCheckIn.Find(Builders<CheckInModel>.Filter.Empty);
        if (data.Any())
        {
            _userCheckIns = data.ToList();
        }
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(3));
    }
    private async void DoWork(object? state)
    {
        _logger.LogInformation($"[{DateTime.Now}] DoWork");
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
                        await TelegramHelper.SentMessage($"[{DateTime.Now}] [{user.UserName}] CheckIn");
                        _isDateTimeCheckOut = true;
                        _isDateTimeCheckIn = false;
                        await CheckInHelper.CheckInOut(user.UserName, user.Passwd);
                        var data = _collectionCheckIn.Find(Builders<CheckInModel>.Filter.Empty);
                        if (data.Any())
                        {
                            _userCheckIns = data.ToList();
                        }
                    }
                    else if (!_isDateTimeCheckIn && DateTimeOffset.Now.TimeOfDay > _dateTimeCheckIn.AddMinutes(-15).TimeOfDay && DateTimeOffset.Now.TimeOfDay < _dateTimeCheckIn.TimeOfDay)
                    {
                        await TelegramHelper.SentMessage($"[{DateTime.Now}] [{user.UserName}] CheckIn");
                        _isDateTimeCheckIn = true;
                        _isDateTimeCheckOut = false;
                        await CheckInHelper.CheckInOut(user.UserName, user.Passwd);
                        var data = _collectionCheckIn.Find(Builders<CheckInModel>.Filter.Empty);
                        if (data.Any())
                        {
                            _userCheckIns = data.ToList();
                        }
                    }
                }
            }
        }
    }
}