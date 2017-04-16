using System;
using System.Runtime.Serialization;

namespace abc.Domain.Exceptions
{
    public class StockNotFoundException : Exception, ISerializable
    {
        public StockNotFoundException(): base(){}

        public StockNotFoundException(string message) : base(message) { }

        public StockNotFoundException(string message, Exception inner) : base(message, inner) { }

        public StockNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
