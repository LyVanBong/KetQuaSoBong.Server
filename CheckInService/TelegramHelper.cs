using RestSharp;

namespace CheckInService;

public static class TelegramHelper
{
    /// <summary>
    /// gửi thông báo lên group telegram
    /// </summary>
    /// <param name="message">Tin nhắn</param>
    /// <returns></returns>
    public static async Task SendMessage(string message)
    {
        try
        {
            var client = new RestClient("https://api.telegram.org/bot5241311795:AAEwwweGdlRbJCow1d5go8xlTuywJo1eIr8/sendMessage?chat_id=-707222451&text=" + message);
            var request = new RestRequest();
            request.Timeout = -1;
            request.Method = Method.Post;
            var response = await client.ExecuteAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine($"[{DateTime.Now}] Thong bao loi: {e.Message}");
        }
    }
}