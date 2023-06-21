using Microsoft.Extensions.Logging;
using System.Text.Json;
using bottlemessage.Models;

namespace bottlemessage.Services
{
    public class JsonMessageService
    {

        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonMessageService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "messages.json"); }
        }

        public List<Message> GetMessages()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var empty = new List<Message>();
            var res = JsonSerializer.Deserialize<List<Message>>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if (res != null)
                return res;
            return empty;
        }

        public string AddMessage(Message toAdd)
        {
            var messages = GetMessages();

            messages.Add(toAdd);



            Random rand = new Random();
            var randmsg = messages.ToArray()[rand.Next(0, messages.Count())];
            while (randmsg._id == toAdd._id)
            {
                randmsg = messages.ToArray()[rand.Next(0, messages.Count())];
            }

            messages.Remove(messages.First(x => x._id == randmsg._id));


            File.WriteAllText(JsonFileName, string.Empty);

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Message>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                messages
            );


            return randmsg._message;
        }

        
    }
}
