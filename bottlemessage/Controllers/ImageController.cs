using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace bottlemessage.Controllers
{
    /// <summary>
    /// Controller that operates images url
    /// </summary>
    public class ImageController : Controller
    {
        public IActionResult Index() => View();

        /// <summary>
        /// Gets list of image urls as strings from Images.txt file 
        /// </summary>
        /// <returns>List<string> containing images url</returns>
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
