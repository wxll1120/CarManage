using System;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class BusinessExceptionHandler
    {
        public static void HandlerException(string message, Exception exception)
        {
            if (exception is BusinessCustomException ||
                exception is DataAccessCustomException ||
                exception is ServiceCustomException)
            {
                throw exception;
            }

            if (exception is DataAccessException || exception is ServiceException)
            {
                throw new BusinessException(exception.Message, exception);
            }

            throw new BusinessException(message, exception);
        }

        public static void HandlerException(string message, ref Exception exception, bool log)
        {
            if (exception is BusinessCustomException ||
                exception is DataAccessCustomException ||
                exception is ServiceCustomException)
            {
                throw exception;
            }

            if (exception is DataAccessException || exception is ServiceException)
            {
                exception = new BusinessException(exception.Message, exception);
            }
            else
                exception = new BusinessException(message, exception);

            if (log)
                ExceptionHandler.HandleException(exception);
            else
                throw exception;
        }
    }
}
