using RestSharp;
using System.Text.RegularExpressions;
using TrucTiepKetQua.net.Server.Services;
using TrucTiepKetQua.net.Shared.Models;

namespace TrucTiepKetQua.net.Server.Helpers;

public static class XoSoMienBac
{
    public static async Task<KqxsMbModel> GetData(string url, DateTime date, ILogger<KetQuaXoSoMienBacService> logger)
    {
        try
        {
            #region loc du lieu

            var kq = new KqxsMbModel();
            kq.NgayQuay = date.ToString("d-M-yyyy");
            var client = new RestClient(string.Format(url, kq.NgayQuay));
            var request = new RestRequest();
            request.Method = Method.Get;
            var response = await client.GetAsync(request);
            var content = response?.Content;

            Regex regex = new Regex(@"class\=\""v\-madb\"">(.*?)<\/span>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            MatchCollection matchCollection = regex.Matches(content);
            if (matchCollection.Any())
            {
                kq.KyHieu = matchCollection[0].Groups[1].Value.Replace(" ", "");

                var dbregex = Regex.Match(content, @"class\=\""v\-gdb \"">(.*?)<\/span>");
                kq.DacBiet = dbregex.Groups[1].Value;

                var g1regex = Regex.Match(content, @"class=""v-g1"">(.*?)</span>");
                kq.G1 = g1regex.Groups[1].Value;

                var g2_0_regex = Regex.Match(content, @"class\=\""v\-g2\-0 \"">(.*?)<\/span>");
                kq.G2_1 = g2_0_regex.Groups[1].Value;
                var g2_1_regex = Regex.Match(content, @"class\=\""v\-g2\-1 \"">(.*?)<\/span>");
                kq.G2_2 = g2_1_regex.Groups[1].Value;

                var g3_0_regex = Regex.Match(content, @"class\=\""v\-g3\-0 \"">(.*?)<\/span>");
                kq.G3_1 = g3_0_regex.Groups[1].Value;
                var g3_1_regex = Regex.Match(content, @"class\=\""v\-g3\-1 \"">(.*?)<\/span>");
                kq.G3_2 = g3_1_regex.Groups[1].Value;
                var g3_2_regex = Regex.Match(content, @"class\=\""v\-g3\-2 \"">(.*?)<\/span>");
                kq.G3_3 = g3_2_regex.Groups[1].Value;
                var g3_3_regex = Regex.Match(content, @"class\=\""v\-g3\-3 \"">(.*?)<\/span>");
                kq.G3_4 = g3_3_regex.Groups[1].Value;
                var g3_4_regex = Regex.Match(content, @"class\=\""v\-g3\-4 \"">(.*?)<\/span>");
                kq.G3_5 = g3_4_regex.Groups[1].Value;
                var g3_5_regex = Regex.Match(content, @"class\=\""v\-g3\-5 \"">(.*?)<\/span>");
                kq.G3_6 = g3_5_regex.Groups[1].Value;

                var g4_0_regex = Regex.Match(content, @"class\=\""v\-g4\-0 \"">(.*?)<\/span>");
                kq.G4_1 = g4_0_regex.Groups[1].Value;
                var g4_1_regex = Regex.Match(content, @"class\=\""v\-g4\-1 \"">(.*?)<\/span>");
                kq.G4_2 = g4_1_regex.Groups[1].Value;
                var g4_2_regex = Regex.Match(content, @"class\=\""v\-g4\-2 \"">(.*?)<\/span>");
                kq.G4_3 = g4_2_regex.Groups[1].Value;
                var g4_3_regex = Regex.Match(content, @"class\=\""v\-g4\-3 \"">(.*?)<\/span>");
                kq.G4_4 = g4_3_regex.Groups[1].Value;

                var g5_0_regex = Regex.Match(content, @"class\=\""v\-g5\-0 \"">(.*?)<\/span>");
                kq.G5_1 = g5_0_regex.Groups[1].Value;
                var g5_1_regex = Regex.Match(content, @"class\=\""v\-g5\-1 \"">(.*?)<\/span>");
                kq.G5_2 = g5_1_regex.Groups[1].Value;
                var g5_2_regex = Regex.Match(content, @"class\=\""v\-g5\-2 \"">(.*?)<\/span>");
                kq.G5_3 = g5_2_regex.Groups[1].Value;
                var g5_3_regex = Regex.Match(content, @"class\=\""v\-g5\-3 \"">(.*?)<\/span>");
                kq.G5_4 = g5_3_regex.Groups[1].Value;
                var g5_4_regex = Regex.Match(content, @"class\=\""v\-g5\-4 \"">(.*?)<\/span>");
                kq.G5_5 = g5_4_regex.Groups[1].Value;
                var g5_5_regex = Regex.Match(content, @"class\=\""v\-g5\-5 \"">(.*?)<\/span>");
                kq.G5_6 = g5_5_regex.Groups[1].Value;

                var g6_0_regex = Regex.Match(content, @"class\=\""v\-g6\-0 \"">(.*?)<\/span>");
                kq.G6_1 = g6_0_regex.Groups[1].Value;
                var g6_1_regex = Regex.Match(content, @"class\=\""v\-g6\-1 \"">(.*?)<\/span>");
                kq.G6_2 = g6_1_regex.Groups[1].Value;
                var g6_2_regex = Regex.Match(content, @"class\=\""v\-g6\-2 \"">(.*?)<\/span>");
                kq.G6_3 = g6_2_regex.Groups[1].Value;

                var g7_0_regex = Regex.Match(content, @"class\=\""v\-g7\-0 \"">(.*?)<\/span>");
                kq.G7_1 = g7_0_regex.Groups[1].Value;
                var g7_1_regex = Regex.Match(content, @"class\=\""v\-g7\-1 \"">(.*?)<\/span>");
                kq.G7_2 = g7_1_regex.Groups[1].Value;
                var g7_2_regex = Regex.Match(content, @"class\=\""v\-g7\-2 \"">(.*?)<\/span>");
                kq.G7_3 = g7_2_regex.Groups[1].Value;
                var g7_3_regex = Regex.Match(content, @"class\=\""v\-g7\-3 \"">(.*?)<\/span>");
                kq.G7_4 = g7_3_regex.Groups[1].Value;

                #endregion loc du lieu

                if (!string.IsNullOrEmpty(kq.G7_4) && !string.IsNullOrEmpty(kq.G7_3))
                {
                    return kq;
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        return null;
    }
}