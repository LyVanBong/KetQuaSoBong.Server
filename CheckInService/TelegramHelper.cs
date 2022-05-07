using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace CheckInService;

public static class TelegramHelper
{
    private static string _accessToken = "5241311795:AAEwwweGdlRbJCow1d5go8xlTuywJo1eIr8";
    private static string _chatId = "-707222451";
    /// <summary>
    /// Gửi ảnh bằng bot telegram
    /// </summary>
    /// <param name="imageMessage">Ảnh</param>
    /// <param name="driver">Google chrome</param>
    /// <param name="textMessage">Caption</param>
    /// <returns></returns>
    public static async Task SentImage(Stream imageMessage = null, ChromeDriver driver = null, string textMessage = "")
    {
        try
        {
            var botClient = new TelegramBotClient(_accessToken);

            if (driver != null)
            {
                Screenshot screenshot = driver.GetScreenshot();
                var data = screenshot.AsBase64EncodedString;
                var bytes = Convert.FromBase64String(data);
                var content = new StreamContent(new MemoryStream(bytes));
                imageMessage = content.ReadAsStream();
            }

            Message message = await botClient.SendPhotoAsync(chatId: _chatId, photo: new InputOnlineFile(imageMessage), caption: textMessage);
        }
        catch (Exception e)
        {
            SentMessage($"[{DateTime.Now}] Thong bao loi: {e.Message}");
        }
    }
    /// <summary>
    /// gửi thông báo lên group telegram
    /// </summary>
    /// <param name="message">Tin nhắn</param>
    /// <returns></returns>
    public static async Task SentMessage(string text)
    {
        try
        {
            // Cach gui message bot tele bang rest full api chinh hang
            // https://api.telegram.org/bot5241311795:AAEwwweGdlRbJCow1d5go8xlTuywJo1eIr8/sendMessage?chat_id=-707222451&text=message
            var botClient = new TelegramBotClient(_accessToken);

            Message message = await botClient.SendTextMessageAsync(chatId: _chatId, text: text);
        }
        catch (Exception e)
        {
            SentMessage($"[{DateTime.Now}] Thong bao loi: {e.Message}");
        }
    }
}