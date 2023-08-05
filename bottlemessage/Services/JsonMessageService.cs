using Microsoft.Extensions.Logging;
using System.Text.Json;
using bottlemessage.Models;

namespace bottlemessage.Services
{
    public class JsonMessageService
    {

        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonMessageService(IWebHostEnvironment webHostEnvironment) => WebHostEnvironment = webHostEnvironment;

        /// <summary>
        /// Sets path to the json file
        /// </summary>
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "messages.json");

        /// <summary>
        /// Gets message from json file
        /// </summary>
        /// <returns>Message saved in json file if it is now empty</returns>
        public Message GetMessage()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var empty = new Message();
            var res = JsonSerializer.Deserialize<Message>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if (res != null)
                return res;
            return empty;
        }

        /// <summary>
        /// Puts new users message into json
        /// </summary>
        /// <param name="toAdd">Message to add to json</param>
        /// <returns>Previous message from json if ip is different from previous</returns>
        public string AddMessage(Message toAdd)
        {
            var message = GetMessage();

            if (message._id == toAdd._id || message._id == "")
            {
                return "Sorry it looks like there is no new message for you\nMaybe tell your friends about this site?";
            }

            var result = message;
            //Remove previous message and add new to the json
            message = toAdd;


            File.WriteAllText(JsonFileName, string.Empty);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<Message>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                message
            );


            return result._message;
        }

        
    }
}
