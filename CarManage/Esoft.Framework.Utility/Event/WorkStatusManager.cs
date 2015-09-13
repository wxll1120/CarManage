using System;
using System.Collections.Generic;
using System.Threading;

namespace Esoft.Framework.Utility.Event
{
    /// <summary>
    /// 工作状态通知管理类
    /// </summary>
    public class WorkStatusManager : IDisposable
    {
        #region 私有变量

        /// <summary>
        /// 开始工作时间
        /// </summary>
        private long startTime;

        ///// <summary>
        ///// 事件列表
        ///// </summary>
        //private EventHandlerList eventHandlerList;

        /// <summary>
        /// 同步方式
        /// </summary>
        private Esoft.Framework.Utility.Event.WorkSyncType syncType;

        #endregion

        public WorkStatusManager()
        {
            //eventHandlerList = EventHandlerList.Synchronized(new EventHandlerList());
            Init();
        }

        static WorkStatusManager()
        {
            //ReportWorkStatusEventKey = new object();
        }


        #region 事件

        #region

        ///// <summary>
        ///// 工作进度状态事件标识
        ///// </summary>
        //private static readonly object ReportWorkStatusEventKey;

        ///// <summary>
        ///// 工作进度通知事件
        ///// </summary>
        //public event EventHandler<ReportWorkStatusEventArgs> OnReportWorkStatus
        //{
        //    add
        //    {
        //        eventHandlerList.AddHandler(ReportWorkStatusEventKey, value);
        //    }

        //    remove
        //    {
        //        eventHandlerList.RemoveHandler(ReportWorkStatusEventKey, value);
        //    }
        //}

        //protected virtual void PostReportWorkStatus(string workStatus)
        //{
        //    ReportWorkStatusEventArgs e = new ReportWorkStatusEventArgs(
        //        workStatus, TotalWork, CurrentWork, ProcessTime, null);

        //    Delegate workDelegate = eventHandlerList[ReportWorkStatusEventKey];

        //    if (workDelegate == null)
        //        return;

        //    if (SyncContext == null)
        //    {
        //        workDelegate.DynamicInvoke(this, e);
        //    }
        //    else
        //    {
        //        SendOrPostCallback callback = new SendOrPostCallback(
        //            InvokeReportWorkStatus);

        //        SyncContext.Send(callback, e);
        //    }
        //}

        //private void InvokeReportWorkStatus(object state)
        //{
        //    ReportWorkStatusEventArgs e = state as ReportWorkStatusEventArgs;

        //    eventHandlerList.DynamicInvoke(ReportWorkStatusEventKey, this, e);
        //}

        #endregion

        /// <summary>
        /// 工作开始事件
        /// </summary>
        public event EventHandler<ReportWorkStatusEventArgs> OnWorkStart;

        /// <summary>
        /// 工作进度通知事件
        /// </summary>
        public event EventHandler<ReportWorkStatusEventArgs> OnReportWorkStatus;

        /// <summary>
        /// 工作结束事件
        /// </summary>
        public event EventHandler<ReportWorkStatusEventArgs> OnWorkEnd;

        /// <summary>
        /// 工作完成事件
        /// </summary>
        public event EventHandler<WorkerCompletedEventArgs> OnWorkComplete;

        /// <summary>
        /// 工作状态通知事件
        /// </summary>
        public event EventHandler<ReportWorkStatusEventArgs> OnReportAccountStatus;

        #endregion

        #region 属性/公有变量

        /// <summary>
        /// 异步操作UI上下文
        /// </summary>
        public SynchronizationContext SyncContext { get; set; }

        /// <summary>
        /// 当前工作进度
        /// </summary>
        public int CurrentWork { get; set; }

        /// <summary>
        /// 总工作量
        /// </summary>
        public int TotalWork { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// 是否繁忙
        /// </summary>
        public bool IsBusy
        {
            get { return false; }
            set { IsBusy = value; }
        }

        /// <summary>
        /// 处理时间
        /// </summary>
        protected TimeSpan ProcessTime
        {
            get
            {
                return new TimeSpan(DateTime.Now.Ticks - startTime);
            }
        }


        protected object UserState
        {
            get { return Guid.NewGuid(); }
        }

        #endregion

        /// <summary>
        /// 初始化工作状态管理对象
        /// </summary>
        public void Init()
        {
            Init(WorkSyncType.Sync);
        }

        /// <summary>
        /// 初始化工作状态管理对象
        /// </summary>
        /// <param name="syncType">同步方式</param>
        public void Init(WorkSyncType syncType)
        {
            startTime = DateTime.Now.Ticks;
            CurrentWork = 0;
            TotalWork = 0;

            this.syncType = syncType;

            if (this.syncType.Equals(WorkSyncType.Async))
                SyncContext = SynchronizationContext.Current;
            else
                SyncContext = null;
        }


        public void Reset()
        {
            startTime = DateTime.Now.Ticks;
            CurrentWork = 0;
            TotalWork = 0;
        }

        /// <summary>
        /// 取消工作
        /// </summary>
        public virtual void Cancel()
        {
            IsCancel = true;
        }

        /// <summary>
        /// 调用工作开始事件
        /// </summary>
        /// <param name="status"></param>
        public virtual void PostReportWorkStart(string status)
        {
            PostReportWorkStart(status, null);
        }

        /// <summary>
        /// 引发工作开始事件
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data"></param>
        public virtual void PostReportWorkStart(string status, object data)
        {
            if (OnWorkStart == null)
                return;

            ReportWorkStatusEventArgs e = new ReportWorkStatusEventArgs(
                status, TotalWork, CurrentWork, ProcessTime, null);

            e.Tag = data;

            if (SyncContext == null)
                OnWorkStart(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkStart), e);
        }


        public virtual void PostReportWorkStart(ReportWorkStatusEventArgs e)
        {
            if (OnWorkStart == null)
                return;

            if (SyncContext == null)
                OnWorkStart(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkStart), e);
        }

        /// <summary>
        /// 异步调用工作开始事件
        /// </summary>
        /// <param name="state"></param>
        private void InvokeReportWorkStart(object state)
        {
            ReportWorkStatusEventArgs e = state as ReportWorkStatusEventArgs;

            OnWorkStart(this, e);
        }

        /// <summary>
        /// 调用工作进度通知事件
        /// </summary>
        /// <param name="status"></param>
        public virtual void PostReportWorkStatus(string status)
        {
            PostReportWorkStatus(status, null);
        }

        /// <summary>
        /// 引发工作进度通知事件
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data"></param>
        public virtual void PostReportWorkStatus(string status, object data)
        {
            if (OnReportWorkStatus == null)
                return;

            ReportWorkStatusEventArgs e = new ReportWorkStatusEventArgs(
                status, TotalWork, CurrentWork, ProcessTime, null);

            e.Tag = data;

            if (SyncContext == null)
                OnReportWorkStatus(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkStatus), e);
        }

        /// <summary>
        /// 引发工作进度通知事件
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data"></param>
        public virtual void PostReportWorkStatus(string status, object data, 
            bool result)
        {
            if (OnReportWorkStatus == null)
                return;

            ReportWorkStatusEventArgs e = new ReportWorkStatusEventArgs(
                status, TotalWork, CurrentWork, ProcessTime, null);

            e.Tag = data;
            e.Result = result;

            if (SyncContext == null)
                OnReportWorkStatus(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkStatus), e);
        }

        public virtual void PostReportWorkStatus(ReportWorkStatusEventArgs e)
        {
            if (OnReportWorkStatus == null)
                return;

            if (SyncContext == null)
                OnReportWorkStatus(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkStatus), e);
        }

        /// <summary>
        /// 异步调用工作进度通知事件
        /// </summary>
        /// <param name="state"></param>
        private void InvokeReportWorkStatus(object state)
        {
            ReportWorkStatusEventArgs e = state as ReportWorkStatusEventArgs;

            OnReportWorkStatus(this, e);
        }

        /// <summary>
        /// 调用工作结束事件
        /// </summary>
        /// <param name="status"></param>
        public virtual void PostReportWorkEnd(string status)
        {
            PostReportWorkEnd(status, null);
        }

        /// <summary>
        /// 引发工作结束事件
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data"></param>
        public virtual void PostReportWorkEnd(string status, object data)
        {
            if (OnWorkEnd == null)
                return;

            ReportWorkStatusEventArgs e = new ReportWorkStatusEventArgs(
                status, TotalWork, CurrentWork, ProcessTime, null);

            e.Tag = data;

            if (SyncContext == null)
                OnWorkEnd(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkEnd), e);
        }

        /// <summary>
        /// 引发工作结束事件
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data"></param>
        public virtual void PostReportWorkEnd(string status, object data, bool result)
        {
            if (OnWorkEnd == null)
                return;

            ReportWorkStatusEventArgs e = new ReportWorkStatusEventArgs(
                status, TotalWork, CurrentWork, ProcessTime, null);

            e.Tag = data;
            e.Result = result;

            if (SyncContext == null)
                OnWorkEnd(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkEnd), e);
        }

        public virtual void PostReportWorkEnd(ReportWorkStatusEventArgs e)
        {
            if (OnWorkEnd == null)
                return;

            if (SyncContext == null)
                OnWorkEnd(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeReportWorkEnd), e);
        }

        /// <summary>
        /// 异步调用工作结束事件
        /// </summary>
        /// <param name="state"></param>
        private void InvokeReportWorkEnd(object state)
        {
            ReportWorkStatusEventArgs e = state as ReportWorkStatusEventArgs;

            OnWorkEnd(this, e);
        }

        /// <summary>
        /// 引发工作完成事件
        /// </summary>
        /// <param name="exception">异常</param>
        public virtual void PostReportWorkComplete(Exception exception)
        {
            WorkerCompletedEventArgs e = new WorkerCompletedEventArgs(
                null, exception, IsCancel, ProcessTime);

            PostReportWorkComplete(e);
        }

        /// <summary>
        /// 引发工作完成事件
        /// </summary>
        /// <param name="e">工作完成事件参数</param>
        public virtual void PostReportWorkComplete(WorkerCompletedEventArgs e)
        {
            if (OnWorkComplete == null)
                return;

            if (SyncContext == null)
                OnWorkComplete(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeWorkComplete), e);
        }

        private void InvokeWorkComplete(object state)
        {
            WorkerCompletedEventArgs e = state as WorkerCompletedEventArgs;

            OnWorkComplete(this, e);
        }

        protected virtual void PostReportAccountWorkStatus(string workStatus)
        {
            ReportWorkStatusEventArgs e = new ReportWorkStatusEventArgs(
                workStatus, TotalWork, CurrentWork, ProcessTime, UserState);

            PostReportAccountWorkStatus(e);
        }

        public virtual void PostReportAccountWorkStatus(ReportWorkStatusEventArgs e)
        {
            if (OnReportAccountStatus == null)
                return;

            if (SyncContext == null)
                OnReportAccountStatus(this, e);
            else
                SyncContext.Send(new SendOrPostCallback(InvokeAccountWorkStatus), e);
        }

        private void InvokeAccountWorkStatus(object state)
        {
            ReportWorkStatusEventArgs e = state as ReportWorkStatusEventArgs;

            OnReportAccountStatus(this, e);
        }

        protected virtual void Dispose(bool disposing)
        {
            SyncContext = null;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
