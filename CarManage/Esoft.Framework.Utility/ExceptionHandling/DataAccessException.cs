using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class DataAccessException : BaseException, ISerializable
    {
        public DataAccessException()
            : base()
        { 
            
        }

        public DataAccessException(string message)
            : base(message)
        { 
            
        }

        public DataAccessException(string message, System.Exception innerException)
            : base(message, innerException)
        { 
            
        }

        public DataAccessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        { 
            
        }
    }
}
