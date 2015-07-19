using System;

namespace Esoft.Framework.Utility.ExceptionHandling
{
    public class UserInterfaceExceptionHandler
    {
        public static void HandlerException(string message, ref Exception exception)
        {
            if (exception is BusinessCustomException ||
                exception is DataAccessCustomException ||
                exception is ServiceCustomException)
            {
                return;
            }

            if (exception is DataAccessException || exception is ServiceException ||
                exception is BusinessException)
            {
                exception = new UserInterfaceException(exception.Message, exception);
            }
            else
                exception = new UserInterfaceException(message, exception);

            ExceptionHandler.HandleException(exception);
        }

        public static void HandlerException(string message, ref Exception exception, bool log)
        {
            if (exception is BusinessCustomException ||
                exception is DataAccessCustomException ||
                exception is ServiceCustomException)
            {
                return;
            }

            if (exception is DataAccessException || exception is ServiceException ||
                exception is BusinessException)
            {
                exception = new UserInterfaceException(exception.Message, exception);
            }
            else
                exception = new UserInterfaceException(message, exception);

            if (log)
                ExceptionHandler.HandleException(exception);
        }
    }
}