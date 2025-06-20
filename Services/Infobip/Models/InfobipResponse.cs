using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infobip.Models
{
    public class InfobipResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class InfobipSmsResponse
    {
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public string To { get; set; }
        public int MessageCount { get; set; }
        public string MessageId { get; set; }
        public Status Status { get; set; }
    }

    public class Status
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
