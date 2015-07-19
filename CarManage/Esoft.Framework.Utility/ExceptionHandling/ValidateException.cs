using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class ValidateException : BaseException, ISerializable
    {
        public ValidateException()
            : base()
        {

        }

        public ValidateException(string message)
            : base(message)
        {

        }

        public ValidateException(string message, 
            System.Exception innerException)
            : base(message, innerException)
        {

        }

        public ValidateException(SerializationInfo serializationInfo, 
            StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public Dictionary<string, string> ErrorCollection { get; set; }
    }
}
