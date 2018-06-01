using System;

namespace OrderBook.Domain
{
    public class ErrorDbException : ApplicationException
    {
        private ErrorMessage _error;

        public ErrorDbException()
        { }
        public ErrorDbException(string message, string detail)
            : base(message)
        {
            _error = new ErrorMessage(message, detail);
        }

        public ErrorDbException(ErrorMessage error)
            : base (error.Message)
        {
            _error = error;
        }

        public ErrorMessage Error
        {
            get
            {
                return _error;
            }
        }
    }
}
