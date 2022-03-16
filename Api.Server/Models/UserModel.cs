using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class UserModel
    {
        [BsonId]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NumberPhone { get; set; }
        public string BirthDay { get; set; }
        public int Sex { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}