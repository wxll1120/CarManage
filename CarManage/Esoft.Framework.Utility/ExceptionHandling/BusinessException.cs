using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class BusinessException : BaseException, ISerializable
    {
        public BusinessException()
            : base()
        { 
            
        }

        public BusinessException(string message)
            : base(message)
        { 
            
        }

        public BusinessException(string message, System.Exception innerException)
            : base(message, innerException)
        { 
            
        }

        public BusinessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        { 
            
        }
    }
}
