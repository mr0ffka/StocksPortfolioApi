
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace StocksPortfolio.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public BadRequestException(string message)
            : base(message)
        {

        }

        public BadRequestException(string message, ValidationResult result)
            : base(message)
        {
            Errors = result.ToDictionary();
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
