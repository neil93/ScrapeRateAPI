using Quartz;
using Common.Logging;
using ScrapeRateAPI.BLL;

namespace ScrapeRateAPI.Job
{
    public class ScheduleJob : IJob
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ScheduleJob));

        void IJob.Execute(IJobExecutionContext context)
        {
            var result = ScrapeTaiwanBank.StartScrape();

            if (!string.IsNullOrEmpty(result))
            {
                _log.Info(result);
                SendMessageToChannel.Instance.SendMessage(result);

            }
        }
    }
}