using RestSharp;
using System.Text.RegularExpressions;

namespace CrawDataXoSo.Helpers;

public static class LichBongDa
{
    public static async Task GetData()
    {
        try
        {
            var client = new RestClient("https://baomoi.com/soccer/m");
            var request = new RestRequest();
            request.Method = Method.Get;
            var response = await client.GetAsync(request);
            var html = response?.Content;
            var regexContent = Regex.Match(html,
                @"<div class=""s-list"">(.*?)<div class=""fyi fyi--bottom nocontent robots-nocontent""></div>");
            var content = regexContent.Groups[1].Value;
            Console.WriteLine(html);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}