using RestSharp;
using Services.Infobip.Models;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Services.Infobip
{
    public class InfobipService
    {
        public InfobipService() { }
        public async Task<InfobipResponse> SendSMS(string recipient, string message)
        {
            try
            {
                var options = new RestClientOptions("https://8k6dyr.api.infobip.com")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/sms/2/text/advanced", Method.Post);
                request.AddHeader("Authorization", "App 2c67f1f653353115ac3a9e586926cefa-19048194-02c0-447e-8ad6-ab4a2f153b0b");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                // Build the body dynamically
                var bodyObj = new
                {
                    messages = new[]
                    {
                new
                {
                    destinations = new[]
                    {
                        new { to = recipient }
                    },
                    from = "ServiceSMS", // should be an approved sender ID
                    text = message
                }
            }
                };

                var jsonBody = JsonConvert.SerializeObject(bodyObj);
                request.AddStringBody(jsonBody, DataFormat.Json);

                RestResponse response = await client.ExecuteAsync(request);
                var parsedResponse = JsonConvert.DeserializeObject<InfobipSmsResponse>(response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new InfobipResponse
                    {
                        Success = true,
                        Message = "SMS sent successfully!"
                    };
                }
                else
                {
                    return new InfobipResponse
                    {
                        Success = false,
                        Message = parsedResponse?.Messages?.FirstOrDefault()?.Status?.Description ?? "Some error occurred!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new InfobipResponse
                {
                    Success = false,
                    Message = ex.Message ?? "Some error occurred!"
                };
            }
        }

        public async Task<InfobipResponse> SendWhatsappMessage(string recipient, string message)
        {
            try
            {
                var options = new RestClientOptions("https://8k6dyr.api.infobip.com")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/whatsapp/1/message/template", Method.Post);
                request.AddHeader("Authorization", "App 2c67f1f653353115ac3a9e586926cefa-19048194-02c0-447e-8ad6-ab4a2f153b0b");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                var bodyObj = new
                {
                    messages = new[]
                    {
                new
                {
                    from = "447860099299", // your WhatsApp sender number
                    to = recipient,
                    messageId = Guid.NewGuid().ToString(), // generate a unique ID
                    content = new
                    {
                        templateName = "test_whatsapp_template_en", // must match approved template
                        templateData = new
                        {
                            body = new
                            {
                                placeholders = new[] { message }
                            }
                        },
                        language = "en"
                    }
                }
            }
                };

                var jsonBody = JsonConvert.SerializeObject(bodyObj);
                request.AddStringBody(jsonBody, DataFormat.Json);

                RestResponse response = await client.ExecuteAsync(request);

                var parsedResponse = JsonConvert.DeserializeObject<InfobipSmsResponse>(response.Content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new InfobipResponse
                    {
                        Success = true,
                        Message = "WhatsApp message sent successfully!"
                    };
                }
                else
                {
                    return new InfobipResponse
                    {
                        Success = false,
                        Message = parsedResponse?.Messages?.FirstOrDefault()?.Status?.Description ?? "Some error occurred!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new InfobipResponse
                {
                    Success = false,
                    Message = ex.Message ?? "Some error occurred!"
                };
            }
        }
    }
}
