using Configurations;
using Models.Chats;
using MongoDB.Driver;

namespace Api.BackgroundServices;

public class ClearMessageService : IHostedService, IDisposable
{
    private Timer _timer = null!;
    private readonly IMongoCollection<ChatBongDa> _collectionBd;
    private readonly IMongoCollection<ChatXoSo> _collectionXs;
    private readonly IMongoDatabase _database;
    private DateTimeOffset _timeCheck = new DateTimeOffset();

    public ClearMessageService()
    {
        MongoClient mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
        _database = mongo.GetDatabase("Kqxs");
        _collectionXs = _database.GetCollection<ChatXoSo>(nameof(ChatXoSo));
        _collectionBd = _database.GetCollection<ChatBongDa>(nameof(ChatBongDa));
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(state => DoWork(state), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    private Task DoWork(object? state)
    {
        if (DateTimeOffset.Now.Hour == 0 && DateTimeOffset.Now.Date != _timeCheck.Date)
        {
            _timeCheck = DateTimeOffset.Now;
            _database.DropCollection(nameof(ChatXoSo));
            _database.DropCollection(nameof(ChatBongDa));
        }

        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}