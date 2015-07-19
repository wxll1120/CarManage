using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace Esoft.Framework.Utility.Event
{
    /// <summary>
    /// 事件列表, 非线程安全
    /// </summary>
    public class EventHandlerList : IDisposable
    {
        /// <summary>
        /// 用于保存"事件键/委托值"对的私有散列表 
        /// </summary>
        private Dictionary<object, Delegate> eventList =
            new Dictionary<object, Delegate>();

        /// <summary>
        /// 一个索引器，用获取或设置与传入的事件对象的散列键相关联的委托
        /// </summary>
        /// <param name="eventKey"></param>
        /// <returns></returns> 
        public virtual Delegate this[object eventKey]
        {
            //如果对象不在集合中，则返回null 
            get
            {
                if (eventList.ContainsKey(eventKey))
                {
                    return eventList[eventKey];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                eventList[eventKey] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventKey"></param>
        public bool Contains(object eventKey)
        {
            return eventList.ContainsKey(eventKey);
        }

        /// <summary>
        /// 指定的事件对象的散列刍对应的委托链表上添加/组全一个委托实例
        /// </summary>
        /// <param name="eventKey"></param>
        /// <param name="handler"></param> 
        public virtual void AddHandler(object eventKey, Delegate handler)
        {
            if (eventList.ContainsKey(eventKey))
            {
                eventList[eventKey] = Delegate.Combine(eventList[eventKey], handler);
            }
            else
            {
                eventList[eventKey] = handler;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventKey"></param>
        /// <param name="handler"></param>
        public virtual void RemoveHandler(object eventKey, Delegate handler)
        {
            if (this.eventList.ContainsKey(eventKey))
            {
                eventList[eventKey] = Delegate.Remove(eventList[eventKey], handler);
            }
        }

        /// <summary>
        /// 在指定的事件对象散列键盘对应的委托链表上触发事件 
        /// </summary>
        /// <param name="eventKey"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DynamicInvoke(object eventKey, object sender, EventArgs e)
        {
            try
            {

                if (eventList.ContainsKey(eventKey))
                {
                    eventList[eventKey].DynamicInvoke(new object[] { sender, e });
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 返回一个对传入的EventHandlerList对象的线程安全的封装 
        /// </summary>
        /// <param name="eventList"></param>
        /// <returns></returns>
        public static EventHandlerList Synchronized(EventHandlerList eventList)
        {
            if (eventList == null)
            {
                throw new ArgumentNullException("eventList");
            }
            return new SyncEventHandlerList(eventList);
        }

        #region IDisposable 成员

        /// <summary>
        /// 释放对象以使散列表占用的内存资源在下一次垃圾收集中被回收，
        /// 从而阻止垃圾收集器提升其性能 
        /// </summary>
        public void Dispose()
        {
            eventList = null;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private class SyncEventHandlerList : EventHandlerList
        {
            /// <summary>
            /// 
            /// </summary>
            private EventHandlerList syncEventHandlerList = null;

            #region 构造函数

            private SyncEventHandlerList()
            {

            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="eventList"></param>
            public SyncEventHandlerList(EventHandlerList eventList)
            {
                syncEventHandlerList = eventList;

                //释放基类中的散列表对象
                base.Dispose();
            }

            #endregion

            /// <summary>
            /// 线程安全的索引器
            /// </summary>
            /// <param name="eventKey"></param>
            /// <returns></returns>
            public override Delegate this[object eventKey]
            {
                [MethodImpl(MethodImplOptions.Synchronized)]
                get
                {
                    return syncEventHandlerList[eventKey];
                }

                [MethodImpl(MethodImplOptions.Synchronized)]
                set
                {
                    syncEventHandlerList[eventKey] = value;
                }
            }

            /// <summary>
            /// 线程安全的AddHander方法
            /// </summary>
            /// <param name="eventKey"></param>
            /// <param name="handler"></param> 
            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void AddHandler(object eventKey, Delegate handler)
            {
                syncEventHandlerList.AddHandler(eventKey, handler);
            }

            /// <summary>
            /// 线程安全的RemoveHandler方法
            /// </summary>
            /// <param name="eventKey"></param>
            /// <param name="handler"></param>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void RemoveHandler(object eventKey, Delegate handler)
            {
                syncEventHandlerList.RemoveHandler(eventKey, handler);
            }

            /// <summary>
            /// 线程安全的Fire方法 
            /// 该方法首先从events 散列表中找到一个委托链表，
            /// 然后调用该委托链表上封装的所有的回调方法 
            /// </summary>
            /// <remarks>因为散列表中可能包含有各种各样的不同的委托类型。所以我们不可能在编译进行类型安全 
            /// * 的委托调用。因此，我们使用了System.Delegate 的DynamicInvoke方法，交将回调方法的参数 
            /// * 组全为一个对象数组传递给它。 
            /// * DynamicInvoke方法在内部会检测回调方法期望的参数和我们传递的参数 
            /// * 是否匹配。如果匹配，回调方法将被调用 
            /// * 
            /// * 否则。DynamicInvoke方法将抛出一个异常. </remarks>
            /// <param name="eventKey"></param>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void DynamicInvoke(object eventKey, object sender, EventArgs e)
            {
                syncEventHandlerList.DynamicInvoke(eventKey, sender, e);
            }
        }
    }
}
