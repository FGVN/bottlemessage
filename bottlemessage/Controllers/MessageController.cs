using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bottlemessage.Services;
using bottlemessage.Models;

namespace bottlemessage.Controllers
{
    public class MessageController : Controller
    {
        public MessageController(JsonMessageService messageService)
        {
            MessageService = messageService;
        }

        public JsonMessageService MessageService { get; }


        public string AddMessage(Message toAdd)
        {
            if (toAdd._id != "")
                return MessageService.AddMessage(toAdd);
            return "";
        }



    }
}
