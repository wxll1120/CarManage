using System;
using System.Collections.Generic;

namespace Esoft.Framework.Utility.Event
{
    /// <summary>
    /// 任务完成事件所需的参数
    /// </summary>
    public class WorkerCompletedEventArgs : System.ComponentModel.RunWorkerCompletedEventArgs
    {
        /// <summary>
        /// 任务完成事件参数构造函数
        /// </summary>
        /// <param name="result"></param>
        /// <param name="error"></param>
        /// <param name="cancelled"></param>
        /// <param name="completedTime"></param>
        public WorkerCompletedEventArgs(Object result, Exception error, Boolean cancelled, TimeSpan completedTime)
            : base(result, error, cancelled)
        {
            this.processTime = completedTime;
        }

        /// <summary>
        /// 完成任务耗费时间
        /// </summary>
        private TimeSpan processTime;

        /// <summary>
        /// 完成任务耗费时间
        /// </summary>
        public TimeSpan ProcessTime
        {
            get { return processTime; }
            set { processTime = value; }
        }

        private Object tag;

        /// <summary>
        /// 
        /// </summary>
        public Object Tag
        {
            get { return this.tag; }
            set { this.tag = value; }
        }
    }
}
