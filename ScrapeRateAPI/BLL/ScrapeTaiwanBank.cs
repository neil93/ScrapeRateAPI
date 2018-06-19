using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Common.Logging;
using HtmlAgilityPack;

namespace ScrapeRateAPI.BLL
{
    public class ScrapeTaiwanBank
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ScrapeTaiwanBank));

        public static string StartScrape()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string url = "http://rate.bot.com.tw/xrt?Lang=zh-TW";
                var htmlWeb = new HtmlWeb();
                var doc = htmlWeb.Load(url);
                var nodes = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/main/div[4]/table").InnerHtml;
                HtmlDocument hdc = new HtmlDocument();
                hdc.LoadHtml(nodes);

                var list = new Dictionary<string, int[]>();
                list.Add("美金", new int[] { 1, 3 });
                list.Add("日幣", new int[] { 8, 3 });
                list.Add("人民幣", new int[] { 19, 3 });
                list.Add("澳幣", new int[] { 4, 3 });
                list.Add("加拿大幣", new int[] { 5, 3 });
                list.Add("紐元", new int[] { 11, 3 });

                sb.AppendLine("");
                sb.AppendLine($"台灣銀行現金匯率：{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}");
                foreach (var key in list)
                {
                    var cashSalePrice = hdc.DocumentNode.SelectSingleNode($"/tbody/tr[{key.Value[0]}]/td[{key.Value[1]}]").InnerText;
                    sb.AppendLine($"{key.Key}:{cashSalePrice}");
                }

                sb.AppendLine($"其它匯率請參考：{url}\r\n通報時間=>週一~週五(AM:09:00~PM:19:00)");
                doc = null;
                return sb.ToString();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return string.Empty;
        }
    }
}