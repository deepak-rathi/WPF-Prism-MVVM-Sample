using System;
using System.Runtime.Serialization;

namespace abc.Domain.Exceptions
{
    public class InValidStockException : Exception, ISerializable
    {
        public InValidStockException(): base(){ }

        public InValidStockException(string message) : base(message) { }

        public InValidStockException(string message, Exception inner) : base(message, inner) { }

        public InValidStockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
