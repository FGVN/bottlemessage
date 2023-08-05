using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bottlemessage.Services;
using bottlemessage.Models;

namespace bottlemessage.Controllers
{
    public class MessageController : Controller
    {
        public MessageController(JsonMessageService messageService) => MessageService = messageService;

        public JsonMessageService MessageService { get; }

        /// <summary>
        /// Sends message to add into json service if id is not null
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns>String of message that has been previusly saved in json</returns>
        public string AddMessage(Message toAdd)
        {
            if (toAdd._id != "")
                return MessageService.AddMessage(toAdd);
            return "";
        }
    }
}
