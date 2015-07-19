using System;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class ServiceExceptionHandler
    {
        public static void HandlerException(string message, Exception exception)
        {
            if (exception is ServiceCustomException ||
                exception is DataAccessCustomException ||
                exception is DataAccessException)
                throw exception;

            throw new ServiceException(message, exception);
        }
    }
}