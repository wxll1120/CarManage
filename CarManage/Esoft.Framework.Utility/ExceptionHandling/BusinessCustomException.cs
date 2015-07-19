using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class BusinessCustomException : BaseException, ISerializable
    {
        public BusinessCustomException()
            : base()
        {

        }

        public BusinessCustomException(string message)
            : base(message)
        {

        }

        public BusinessCustomException(string message, 
            System.Exception innerException)
            : base(message, innerException)
        {

        }

        public BusinessCustomException(SerializationInfo serializationInfo, 
            StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}