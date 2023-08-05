namespace bottlemessage.Models
{
    /// <summary>
    /// Data structure that describes message
    /// </summary>
    public class Message
    {
        public string _id { get; set; }
        public string _message { get; set; }
        public DateTime _sendDate { get; set; }

        public Message(string id, string message)
        {
            _id = id;
            _message = message;
            _sendDate = DateTime.Now;
        }

        public Message()
        {
            _id = "";
            _message = "";
            _sendDate = DateTime.Now;
        }
    }
}
