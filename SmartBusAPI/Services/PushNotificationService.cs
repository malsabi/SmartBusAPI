﻿namespace SmartBusAPI.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        public async Task<string> SendNotification(string title, string message, int? parentID, int? busID)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var data = new
                {
                    subID = parentID,
                    appId = PushNotificationConsts.APP_ID,
                    appToken = PushNotificationConsts.APP_TOKEN,
                    title,
                    message
                };
                
                HttpResponseMessage response = await client.PostAsJsonAsync(PushNotificationConsts.EndPoint, data);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return "Failed to push notification";
                }
            }
            catch
            {
                return default;
            }
        }
    }
}