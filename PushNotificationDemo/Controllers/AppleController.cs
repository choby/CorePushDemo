using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CorePush.Apple;
using Newtonsoft.Json;

namespace PushNotificationDemo.Controllers
{
    public class AppleController : Controller
    {
        private readonly ILogger<AppleController> _logger;

        public AppleController(ILogger<AppleController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            ApnSettings settings = new ApnSettings();

            settings.ServerType = ApnServerType.Production;
            settings.TeamId = "44KGXK85M8";
            settings.AppBundleIdentifier = "com.ultronnet.seekit24";
            settings.P8PrivateKey = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQg8dK9Q2N3e4XfYW4e" +
                "fn5v5TCnv2MmMSU349XsxVCewJGgCgYIKoZIzj0DAQehRANCAATCm9YLSdOtPS3n" +
                "hhtYV2R7rjbUogF3cl4jD8evA3XHqRrH28YrOeLKFEt73ofFbxUKLvTkup0cNtTo" +
                "GJALzQMu";
            settings.P8PrivateKeyId = "QS47LJVNG3";

            var apn = new ApnSender(settings, httpClient);

            AppleNotification notification = new AppleNotification();
            notification.Aps = new AppleNotification.ApsPayload();
            notification.ExperienceId = "@choby/seekit24";
            notification.Aps.Alert = new AppleNotificationAlert
            {
                Title = "你有一条新的消息",
                Body = "你屁股着火拉✅"
            };

            string deviceToken = "a0ba4576e8be019e88a3d9b4258eb04ebefdb7ad87f9d1fcc9dd727632760d06";
            var response = await apn.SendAsync(notification, deviceToken);


            return Content($"ios push result:{response.IsSuccess.ToString()}");
        }

    }

    public class AppleNotification
    {
        public class ApsPayload
        {
            [JsonProperty("alert")]
            public AppleNotificationAlert Alert { get; set; }
        }

        [JsonProperty("aps")]
        public ApsPayload Aps { get; set; }

        [JsonProperty("experienceId")]
        public string ExperienceId { get; set; }
    }

    public class AppleNotificationAlert
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}