using System.Diagnostics;
using System.Text.RegularExpressions;
using Api.Models;
using RestSharp;

namespace Api.Helpers;

public static class XoSoMienNam
{
    public static async Task<KqxsMnModel> GetData(string url, string date)
    {
        try
        {
            #region loc du lieu

            var kq = new KqxsMnModel();
            kq.NgayQuay = date;
            var client = new RestClient(string.Format(url, kq.NgayQuay));
            var request = new RestRequest();
            request.Method = Method.Get;
            var response = await client.GetAsync(request);
            var content = response?.Content;

            // lấy tên tính thành
            var regexTinhThanh = Regex.Matches(content, @"class=""underline bold"">(.*?)</a>");
            if (regexTinhThanh.Any())
            {
                foreach (Match o in regexTinhThanh)
                {
                    kq.Datas.Add(new KqxsModel()
                    {
                        TenTinhThanh = o.Groups[1].Value,
                    });
                }
                // g8
                var regexG8 = Regex.Matches(content, @"class=""v-g8 "">(.*?)</div>");
                if (regexG8.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG8)
                    {
                        kq.Datas[num].G8 = o.Groups[1].Value;
                        num++;
                    }
                }

                // g7
                var regexG7 = Regex.Matches(content, @"class=""v-g8 "">(.*?)</div>");
                if (regexG7.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG7)
                    {
                        kq.Datas[num].G7_1 = o.Groups[1].Value;
                        num++;
                    }
                }

                // g6
                //v-g6-0
                var regexG60 = Regex.Matches(content, @"class=""v-g6-0 "">(.*?)</div>");
                if (regexG60.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG60)
                    {
                        kq.Datas[num].G6_1 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g6-1
                var regexG61 = Regex.Matches(content, @"class=""v-g6-1 "">(.*?)</div>");
                if (regexG61.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG61)
                    {
                        kq.Datas[num].G6_2 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g6-1
                var regexG62 = Regex.Matches(content, @"class=""v-g6-2 "">(.*?)</div>");
                if (regexG62.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG62)
                    {
                        kq.Datas[num].G6_3 = o.Groups[1].Value;
                        num++;
                    }
                }

                //g5
                var regexG5 = Regex.Matches(content, @"class=""v-g5 "">(.*?)</div>");
                if (regexG5.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG7)
                    {
                        kq.Datas[num].G5_1 = o.Groups[1].Value;
                        num++;
                    }
                }

                // g4
                // v-g4-0
                var regexG40 = Regex.Matches(content, @"class=""v-g4-0 "">(.*?)</div>");
                if (regexG40.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG40)
                    {
                        kq.Datas[num].G4_1 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g4-1
                var regexG41 = Regex.Matches(content, @"class=""v-g4-1 "">(.*?)</div>");
                if (regexG41.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG41)
                    {
                        kq.Datas[num].G4_2 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g4-2
                var regexG42 = Regex.Matches(content, @"class=""v-g4-2 "">(.*?)</div>");
                if (regexG42.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG42)
                    {
                        kq.Datas[num].G4_3 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g4-3
                var regexG43 = Regex.Matches(content, @"class=""v-g4-3 "">(.*?)</div>");
                if (regexG43.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG43)
                    {
                        kq.Datas[num].G4_4 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g4-4
                var regexG44 = Regex.Matches(content, @"class=""v-g4-4 "">(.*?)</div>");
                if (regexG44.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG44)
                    {
                        kq.Datas[num].G4_5 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g4-5
                var regexG45 = Regex.Matches(content, @"class=""v-g4-5 "">(.*?)</div>");
                if (regexG45.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG45)
                    {
                        kq.Datas[num].G4_6 = o.Groups[1].Value;
                        num++;
                    }
                }
                // v-g4-6
                var regexG46 = Regex.Matches(content, @"class=""v-g4-6 "">(.*?)</div>");
                if (regexG46.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG46)
                    {
                        kq.Datas[num].G4_7 = o.Groups[1].Value;
                        num++;
                    }
                }

                // g3
                // g3-v-0
                var regexG30 = Regex.Matches(content, @"class=""v-g3-0 "">(.*?)</div>");
                if (regexG30.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG30)
                    {
                        kq.Datas[num].G3_1 = o.Groups[1].Value;
                        num++;
                    }
                }
                // g3-v-1
                var regexG31 = Regex.Matches(content, @"class=""v-g3-1 "">(.*?)</div>");
                if (regexG30.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG31)
                    {
                        kq.Datas[num].G3_2 = o.Groups[1].Value;
                        num++;
                    }
                }

                // g2
                var regexG2 = Regex.Matches(content, @"class=""v-g2 "">(.*?)</div>");
                if (regexG2.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG2)
                    {
                        kq.Datas[num].G2_1 = o.Groups[1].Value;
                        num++;
                    }
                }

                // g1
                var regexG1 = Regex.Matches(content, @"class=""v-g1 "">(.*?)</div>");
                if (regexG1.Any())
                {
                    var num = 0;
                    foreach (Match o in regexG1)
                    {
                        kq.Datas[num].G1 = o.Groups[1].Value;
                        num++;
                    }
                }

                // dac biet
                var regexdb = Regex.Matches(content, @"class=""v-gdb "">(.*?)</div>");
                if (regexdb.Any())
                {
                    var num = 0;
                    foreach (Match o in regexdb)
                    {
                        kq.Datas[num].DacBiet = o.Groups[1].Value;
                        num++;
                    }
                }
            }

            #endregion loc du lieu

            if (kq.Datas.Any())
            {
                return kq;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        return null;
    }
}