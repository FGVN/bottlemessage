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

        public List<string> _images;

        public Random _random;

        public int _imagesLen;

        public IndexModel(ILogger<IndexModel> logger,
                          JsonMessageService msgservice)
        {
            _logger = logger;
            _messageController = new MessageController(msgservice);
            _random = new Random();
        }

        public void OnGet()
        {
            ImageController imageController = new ImageController();
            _images = imageController.GetImages();

            for (int i = 0; i < _images.ToArray().Length; i++)
            {
                _images[i] = _images[i].Replace("\\", string.Empty);
                Console.WriteLine(i);
            }

            _imagesLen = _images.ToArray().Length;
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