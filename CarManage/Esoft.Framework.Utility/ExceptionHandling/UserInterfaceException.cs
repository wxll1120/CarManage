using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class UserInterfaceException : BaseException, ISerializable
    {
        public UserInterfaceException()
            : base()
        { 
            
        }

        public UserInterfaceException(string message)
            : base(message)
        { 
            
        }

        public UserInterfaceException(string message, System.Exception innerException)
            : base(message, innerException)
        { 
            
        }

        public UserInterfaceException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        { 
            
        }
    }
}
