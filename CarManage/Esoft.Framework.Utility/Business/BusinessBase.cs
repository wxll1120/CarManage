using System;
using System.Collections.Generic;

using Esoft.Framework.Utility.ExceptionHandling;
using Esoft.Framework.Utility.Event;

namespace Esoft.Framework.Utility.Business
{
    public abstract class BusinessBase : WorkStatusManager
    {
        #region 私有变量

        /// <summary>
        /// 验证错误字典
        /// </summary>
        private Dictionary<string, string> errorCollection;

        #endregion
        
        public BusinessBase()
        {
            errorCollection = new Dictionary<string, string>();
        }

        /// <summary>
        /// 开始验证
        /// </summary>
        public virtual void BeginValidate()
        {
            errorCollection.Clear(); 
        }

        /// <summary>
        /// 结束验证
        /// </summary>
        public virtual void EndValidate()
        {
            if (Valid)
                return;

            ValidateException exception = new ValidateException();
            exception.ErrorCollection = errorCollection;

            throw exception;
        }

        /// <summary>
        /// 添加验证错误信息
        /// </summary>
        /// <param name="propertyName">错误属性名</param>
        /// <param name="message">错误信息</param>
        public void AddValidationError(string propertyName, string message)
        {
            errorCollection[propertyName] = message;
        }

        /// <summary>
        /// 添加验证错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public void AddValidationError(string message)
        {
            errorCollection[errorCollection.Count.ToString()] = message;
        }

        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool Valid
        {
            get
            {
                return errorCollection.Count.Equals(0);
            }
        }
    }
}
