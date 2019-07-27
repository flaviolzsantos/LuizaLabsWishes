using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LuizaLabs.Infra.Cross
{
    public class NotContentException : Exception
    {
        public NotContentException()
        {
        }

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
