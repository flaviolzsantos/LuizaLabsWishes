using System;
using System.Runtime.Serialization;

namespace LuizaLabs.Infra.Cross
{
    public class NotContentException : Exception
    {
        public NotContentException(string message) : base(message)
        {
        }

        public NotContentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
