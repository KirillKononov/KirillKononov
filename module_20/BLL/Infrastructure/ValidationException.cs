using System;

namespace BLL.Infrastructure
{
    class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
