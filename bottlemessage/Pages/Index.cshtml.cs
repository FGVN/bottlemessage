using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bottlemessage.Models;
using bottlemessage.Controllers;
using bottlemessage.Services;

namespace bottlemessage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public MessageController _messageController;

        public IndexModel(ILogger<IndexModel> logger,
                          JsonMessageService msgservice)
        {
            _logger = logger;
            _messageController = new MessageController(msgservice);
        }

        public void OnGet()
        {
            if(Request.Cookies["sendDate"] != null)
            {
                
                Console.WriteLine("Diff date: " +
                    (DateTime.Now.Ticks / 10000000
                    - Convert.ToDateTime(Request.Cookies["sendDate"]).Ticks / 10000000));
            }


        }

        public IActionResult OnPost()
        {
            var msg = Request.Form["msg"];
            var ip = Request.HttpContext.Connection.RemoteIpAddress;
            Console.WriteLine(ip);

            var cookieOptions = new CookieOptions();

            cookieOptions.Path = "/";
            if(ip.ToString() != null)
                Response.Cookies.Append("id", ip.ToString(), cookieOptions);

            Response.Cookies.Append("sendDate", DateTime.Now.ToString(), cookieOptions);

            Response.Cookies.Append("retrievedMessage",
                _messageController.AddMessage(new Message(ip.ToString(), msg)),
                cookieOptions);

            return Redirect("/Index");
        }
    }
}