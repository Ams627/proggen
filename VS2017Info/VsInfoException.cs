using System;
using System.Runtime.Serialization;

namespace VS2017Info
{
    [Serializable]
    public class VsInfoException : Exception
    {
        public VsInfoException()
        {
        }

        public VsInfoException(string message) : base(message)
        {
        }

        public VsInfoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VsInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}