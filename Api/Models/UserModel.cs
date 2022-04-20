using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models;

public class UserModel
{
    [BsonElement("_id")]
    public string UserName { get; set; }
    public string Passwd { get; set; }
    public int Sex { get; set; }
    public string NumberPhone { get; set; }
    public string Email { get; set; }
}