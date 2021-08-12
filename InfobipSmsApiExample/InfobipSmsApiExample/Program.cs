using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace InfobipSmsApiExample
{
    class Program
    {
        // register on portal.infobip.com to get the API Key and base URL
        static readonly string BASE_URL = "https://<PUT_YOUR_URL_PREFIX>.api.infobip.com";
        static readonly string API_KEY = "<PUT_YOUR_API_KEY>";
        static readonly string URL_PART = "sms/2/text/advanced";

        static void Main(string[] args)
        {
            Console.WriteLine("Infobip SMS API code examples");

            SendSms();
        }

        static void SendSms()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            // We use "App" for Api Key authntication, check infobip.com/docs/essentials/api-authentication for other options
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", API_KEY);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string destinationNumber = "41793026727"; // Put your number here to get the message on your mobile phone
            
            string jsonBody = $@"
            {{
                ""messages"": [
                {{
                    ""from"": ""InfoSMS"",
                    ""destinations"":
                    [
                        {{
                            ""to"": ""{destinationNumber}""
                        }}
                  ],
                  ""text"": ""Test message sent using Infobip SMS API code example""
                }}
              ]
            }}";

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, URL_PART);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = client.SendAsync(httpRequest).GetAwaiter().GetResult();
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Console.WriteLine(responseContent);
        }
    }
}
