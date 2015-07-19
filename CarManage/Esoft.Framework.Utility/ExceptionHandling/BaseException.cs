using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class BaseException : System.Exception, ISerializable
    {
        public BaseException()
            : base()
        { 
            
        }

        public BaseException(string message)
            : base(message)
        { 
            
        }

        public BaseException(string message, System.Exception innerException)
            : base(message, innerException)
        { 
            
        }

        public BaseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
