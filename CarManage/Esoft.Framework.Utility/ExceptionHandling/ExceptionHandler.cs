using System;

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

using Esoft.Framework.Utility.Configuration;

namespace Esoft.Framework.Utility
{
    public class ExceptionHandler
    {
        public static void HandleException(Exception ex)
        {
            try
            {
                ExceptionPolicy.HandleException(ex, CarManageConfig.Instance.ExceptionPolicy);
            }
            catch
            { 
                
            }
        }
    }
}
