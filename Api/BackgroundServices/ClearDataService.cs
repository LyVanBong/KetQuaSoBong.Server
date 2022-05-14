using Configurations;
using Models.BinhChon;
using Models.Chats;
using MongoDB.Driver;

namespace Api.BackgroundServices;

public class ClearDataService : BackgroundService
{
    private Timer _timer = null!;
    private readonly IMongoDatabase _database;
    private DateTimeOffset _timeChat = new DateTimeOffset();
    private DateTimeOffset _timeBinhChon = new DateTimeOffset();

    public ClearDataService()
    {
        MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
        _database = mongo.GetDatabase("Kqxs");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(state => DoWork(state), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }
    private Task DoWork(object? state)
    {
        if (DateTimeOffset.Now.Hour == 0 && DateTimeOffset.Now.Date != _timeChat.Date)
        {
            _timeChat = DateTimeOffset.Now;
            _database.DropCollection(nameof(ChatXoSo));
            _database.DropCollection(nameof(ChatBongDa));
        }

        if (DateTimeOffset.Now.Hour == 19 && DateTimeOffset.Now.Date != _timeBinhChon.Date)
        {
            _timeBinhChon = DateTimeOffset.Now;
            _database.DropCollection(nameof(BinhChon));
            _database.DropCollection(nameof(LichSuBinhChon));
        }

        return Task.CompletedTask;
    }
}