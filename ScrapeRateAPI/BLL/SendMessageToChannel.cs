using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Common.Logging;
using System.Collections.Specialized;

namespace ScrapeRateAPI.BLL
{
    public class SendMessageToChannel
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SendMessageToChannel));
        private static readonly Lazy<SendMessageToChannel> LazyInstance = new Lazy<SendMessageToChannel>(() => new SendMessageToChannel());

        private static string token = ConfigurationManager.AppSettings["LineToken"];

        private static string sendMessageUri = ConfigurationManager.AppSettings["SendMessageUri"];

        private SendMessageToChannel() { }

        public static SendMessageToChannel Instance { get { return LazyInstance.Value; } }

        public void SendMessage(string message)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Authorization", $"Bearer {token}");
                    NameValueCollection nc = new NameValueCollection();
                    nc["message"] = "test" + message;

                    byte[] bResult = client.UploadValues(sendMessageUri, nc);
                    string resultXML = Encoding.UTF8.GetString(bResult);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}