using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class ServiceCustomException : BaseException, ISerializable
    {
        public ServiceCustomException()
            : base()
        {

        }

        public ServiceCustomException(string message)
            : base(message)
        {

        }

        public ServiceCustomException(string message,
            System.Exception innerException)
            : base(message, innerException)
        {

        }

        public ServiceCustomException(SerializationInfo serializationInfo,
            StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}