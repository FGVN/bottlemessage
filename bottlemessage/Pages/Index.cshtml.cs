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
        //to get all images urls
        public List<string> _images;
        //to randomly select background image from list
        public Random _random;
        //length of list of images
        public int _imagesLen;

        //initializing messages controller and random
        public IndexModel(ILogger<IndexModel> logger,
                          JsonMessageService msgservice)
        {
            _logger = logger;
            _messageController = new MessageController(msgservice);
            _random = new Random();
        }

        //assigning values to images list from controller and its length variable
        public void OnGet()
        {
            ImageController imageController = new ImageController();
            _images = imageController.GetImages();

            for (int i = 0; i < _images.ToArray().Length; i++)
            {
                _images[i] = _images[i].Replace("\\", string.Empty);
            }

            _imagesLen = _images.ToArray().Length;
        }

        //getting msg form value, setting ip address, message value and send date into cookie
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