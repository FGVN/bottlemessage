using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace bottlemessage.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<string> GetImages()
        {
            List<string> Images = new List<string>();


            string currentDirectory = Directory.GetCurrentDirectory();

            string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            string dataDirectory = Path.Combine(currentDirectory, "wwwroot", "data"); 
            string filePath = Path.Combine(dataDirectory, "Images.txt"); 

                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        Images.Add(reader.ReadLine()); 
                    }
                }
            

            return Images;
        }
    }
}
