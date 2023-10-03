using System;
using System.Runtime.Serialization;

namespace MyException
{
    public class ExceptionDataBase : Exception
    {
        public ExceptionDataBase() : base() { }

        public ExceptionDataBase(string message) : base(message) { }

        public ExceptionDataBase(string message, Exception innerException) : base(message, innerException) { }

        public ExceptionDataBase(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
