namespace Api.Server.Configurations;

public static class Configuration
{
    public const string ConnectionStringMongodb = @$"mongodb://{UserNameMongoDb}:{PasswordMongoDb}@103.173.155.213:27017";
    public const string UserNameMongoDb = "root";
    public const string PasswordMongoDb = "passwd12345";
    public const string DatabaseName = "KetQuaSoBong";
}