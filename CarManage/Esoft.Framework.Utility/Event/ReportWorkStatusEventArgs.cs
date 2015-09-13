using System;
using System.ComponentModel;

namespace Esoft.Framework.Utility.Event
{
    public class ReportWorkStatusEventArgs :  ProgressChangedEventArgs
    {
        /// <summary>
        /// 工作状态事件构造函数
        /// </summary>
        /// <param name="workStatus">报告工作状态</param>
        /// <param name="totalWorkCount">总共的工作量</param>
        /// <param name="currentWork">当前的工作进度</param>
        /// <param name="userState">唯一的用户状态</param>
        public ReportWorkStatusEventArgs(string workStatus, int totalWorkCount,
            int currentWork, TimeSpan processTime, Object userState)
            : base(((totalWorkCount == 0) ? 0 : currentWork * 100 / totalWorkCount),
            userState)
        {
            WorkStatus = workStatus;
            TotalWorkCount = totalWorkCount;
            CurrentWork = currentWork;
            ProcessTime = processTime;
        }

        /// <summary>
        /// 获取工作状态
        /// </summary>
        public string WorkStatus { get; set; }

        /// <summary>
        /// 获取总共的工作量
        /// </summary>
        public int TotalWorkCount { get; set; }
        /// <summary>
        /// 获取当前的工作量
        /// </summary>
        public int CurrentWork { get; set; }

        /// <summary>
        /// 完成任务耗费时间
        /// </summary>
        public TimeSpan ProcessTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Object Tag { get; set; }


        public bool Result { get; set; }


        public string OperateType { get; set; }
        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetRemanentTime()
        {
            return new TimeSpan(TotalWorkCount * ProcessTime.Ticks / CurrentWork);
        }
    }
}
