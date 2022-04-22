using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Chats;

public class ChatModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    /// <summary>
    /// Tên người dùng
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Tài khoản đăng nhập
    /// </summary>
    public string UserName { get; set; }

    public string Image { get; set; } =
        "https://img.icons8.com/external-flaticons-flat-flat-icons/344/external-user-web-flaticons-flat-flat-icons-2.png";

    public string DateCreate { get; set; } = DateTimeOffset.Now.ToString("G");
    /// <summary>
    /// Tin nhắn
    /// </summary>
    public string Message { get; set; }
}