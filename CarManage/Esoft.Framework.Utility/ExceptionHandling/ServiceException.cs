using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class ServiceException : BaseException, ISerializable
    {
        public ServiceException()
            : base()
        { 
            
        }

        public ServiceException(string message)
            : base(message)
        { 
            
        }

        public ServiceException(string message, System.Exception innerException)
            : base(message, innerException)
        { 
            
        }

        public ServiceException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        { 
            
        }
    }
}
