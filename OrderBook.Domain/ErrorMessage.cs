using System.Text;

namespace OrderBook.Domain
{
    public class ErrorMessage
    {
        public ErrorMessage()
        { }
        public ErrorMessage(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }

        public string Database { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}