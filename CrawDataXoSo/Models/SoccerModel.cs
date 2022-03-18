using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrawDataXoSo.Models;

public class SoccerModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string DateLive { get; set; }
    public List<CountryModel> Datas { get; set; }
}