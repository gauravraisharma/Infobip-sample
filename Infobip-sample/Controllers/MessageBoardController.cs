using Microsoft.AspNetCore.Mvc;
using Services.Infobip;
using Services.Twilio;

namespace Infobip_sample.Controllers
{
    public class MessageBoardController : Controller
    {
        private readonly IConfiguration _configurationManager;
        InfobipService infobipMessageService;
        TwilioService twilioService;
        private readonly string messageProvider;
        public MessageBoardController(IConfiguration configurationManager)
        {
            infobipMessageService = new InfobipService();
            twilioService = new TwilioService();
            _configurationManager = configurationManager;
            messageProvider = _configurationManager["MessageProvider"]?.ToLower()??"infobip";

        }
        public IActionResult SMS()
        {
            return View(); 
        }

        public IActionResult Whatsapp()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Sms(string recipient, string message)
        {
            if (messageProvider.ToLower()=="twilio")
            {
                var response = await twilioService.SendSMS(recipient,message);
                TempData["Status"] = response.Message;
                TempData["StatusClass"] = response.Success ? "alert-success" : "alert-danger";
                return View();
            }
            else
            {
                var response = await infobipMessageService.SendSMS(recipient,message);
                TempData["Status"] = response.Message;
                TempData["StatusClass"] = response.Success ? "alert-success" : "alert-danger";
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> WhatsApp(string recipient, string message)
        {
            if (messageProvider.ToLower() == "twilio")
            {
                var response = await twilioService.SendWhatsappMessage(recipient,message);
                TempData["Status"] = response.Message;
                TempData["StatusClass"] = response.Success ? "alert-success" : "alert-danger";
                return View();
            }
            else
            {
                var response = await infobipMessageService.SendWhatsappMessage(recipient,message);
                TempData["Status"] = response.Message;
                TempData["StatusClass"] = response.Success ? "alert-success" : "alert-danger";
                return View();
            }
                
        }
    }
}
