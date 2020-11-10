using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorePush.Google;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PushNotificationDemo.Controllers
{
    public class AndroidController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            FcmSettings settings = new FcmSettings();

            settings.SenderId = "557535861881";
            settings.ServerKey = "AAAAgc-6HHk:APA91bGGjo4Xld7-Lg0vRTcORTZWKFIwZaTZnmw4fr21pgusvwobvSCjzUSjj7-7JlaIcQr2GDNptWZQKe5oWuopgkgpmH-aOHe9_Q9eKUHGgUIMhLPJhEtrq-jHPIyDpBLHmWp2tTf8";


            var fcm = new FcmSender(settings, httpClient);

            var notification = new GoogleNotification
            {
                Data = new GoogleNotification.DataPayload
                {
                    ExperienceId = "@choby/seekit24",
                    Message = "安卓测试消息 🆚"
                },
                Priority = "high"
            };

            string deviceToken = "fQVPgfq8TZKrltAwHSB-WI:APA91bFWh5E5PKp_8fdjlkkeE7K6xjoA9CH9Dll-nJBvPwnKDcsNnH2Py5Gf5T7SMna4X0D35_tA8a32ytB29nApEei4Kx78pIsk7md6xfBh-_JffilmRZQSR179e2pc8pbZs4T78ocp";

            FcmResponse response = await fcm.SendAsync(deviceToken, notification);

            return Content($"android push result:{response.IsSuccess().ToString()}");
        }
    }

    public class GoogleNotification
    {
        public class DataPayload
        {
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("experienceId")]
            public string ExperienceId { get; set; }
        }

        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";

        [JsonProperty("data")]
        public DataPayload Data { get; set; }
    }


}
