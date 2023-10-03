using System;
using System.Runtime.Serialization;

namespace MyException
{
    public class ExceptionAttribute : Exception
    {
        public ExceptionAttribute() : base() { }

        public ExceptionAttribute(string message) : base(message) { }

        public ExceptionAttribute(string message, Exception innerException) : base(message, innerException) { }

        public ExceptionAttribute(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
