using System;
using System.Runtime.Serialization;

namespace MyLib.Web.Common
{
    [Serializable]
    internal class MyLibWebRequestException : Exception
    {
        public MyLibWebRequestException()
        {
        }

        public MyLibWebRequestException(string message) : base(message)
        {
        }

        public MyLibWebRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyLibWebRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
