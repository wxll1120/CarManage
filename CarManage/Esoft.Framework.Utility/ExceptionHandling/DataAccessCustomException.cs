using System;
using System.Runtime.Serialization;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class DataAccessCustomException : BaseException, ISerializable
    {
        public DataAccessCustomException()
            : base()
        {

        }

        public DataAccessCustomException(string message)
            : base(message)
        {

        }

        public DataAccessCustomException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }

        public DataAccessCustomException(SerializationInfo serializationInfo, 
            StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}