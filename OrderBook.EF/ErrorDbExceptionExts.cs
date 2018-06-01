using System.Text;
using System.Data.SqlClient;
using OrderBook.Domain;

namespace OrderBook.EF
{
    public static class ErrorDbExceptionExts
    {
        public static ErrorMessage ToErrorMessage(this SqlException ex, string message)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ex.Errors.Count; i++)
            {
                sb.AppendLine(string.Format(@"{0} строка:{1} {2}",
                                            ex.Errors[i].Message,
                                            ex.Errors[i].LineNumber,
                                            ex.Errors[i].Source));
            }

            ErrorMessage msg = new ErrorMessage
            {
                Database = "OrderBook",
                Code = ex.Number,
                Message = message,
                Detail = sb.ToString()
            };

            return msg;
        }
    }
}
