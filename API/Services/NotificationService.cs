using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using OneSignalApi.Api;
using OneSignalApi.Client;
using OneSignalApi.Model;

namespace Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {
        }

        public async Task CreateNotificationAsync() {

            var appConfig = new Configuration();
            
            appConfig.BasePath = "https://onesignal.com/api/v1";
            appConfig.AccessToken = "NDE2MWZlOTktNmViOC00ZDE2LWIwMjItMTBlNDc2MDlhMWE1";
            var apiInstance = new DefaultApi(appConfig);
            var notification = new Notification(appId: "a3709ba4-56a6-4022-98c3-1f9997c1078e")
            {                
                Contents = new StringMap(en: "Hello World from .NET!"),
                IncludedSegments = new List<string> { "Subscribed Users" }
                
                
            };
            var response = await apiInstance.CreateNotificationAsync(notification);
            CreateNotificationSuccessResponse result = apiInstance.CreateNotification(notification);
            Debug.WriteLine(result);
            

            Console.WriteLine($"Notification created for {response.Recipients} recipients");
        }
    }
}