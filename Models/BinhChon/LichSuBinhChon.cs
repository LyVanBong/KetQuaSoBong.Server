using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.BinhChon;

public class LichSuBinhChon
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Num { get; set; }
    public string UserName { get; set; }
    public string DateCreate { get; set; } = DateTimeOffset.Now.ToString("G");
}