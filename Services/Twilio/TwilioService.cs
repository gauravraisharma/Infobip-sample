
using Services.Twilio.Models;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Services.Twilio
{
    public class TwilioService
    {
        public async Task<TwilioResponse> SendSMS(string recipient, string text)
        {
            try
            {
                var accountSid = "AC3f2865c62fbe1a7446db6da1d677e701";
                var authToken = "91ede85206028e8f83b94ad4d7449216";
                TwilioClient.Init(accountSid, authToken);
                var messageOptions = new CreateMessageOptions(
                  new PhoneNumber(recipient));
                messageOptions.From = new PhoneNumber("+12512782989");
                messageOptions.Body = text;
                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);
                return new TwilioResponse
                {
                    Message = "SMS sent successfully!",
                    Success = true,
                };
            }
            catch (Exception e)
            {
                return new TwilioResponse
                {
                    Message = e.Message,
                    Success = false
                };
            }
            
        }

        public async Task<TwilioResponse> SendWhatsappMessage(string recipient, string text)
        {
            try
            {
                var accountSid = "AC3f2865c62fbe1a7446db6da1d677e701";
                var authToken = "91ede85206028e8f83b94ad4d7449216";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                  new PhoneNumber($"whatsapp:{recipient}"));
                messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
                messageOptions.Body = text;


                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);
                return new TwilioResponse
                {
                    Message = "Whatsapp message sent successfully!",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new TwilioResponse
                {
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
