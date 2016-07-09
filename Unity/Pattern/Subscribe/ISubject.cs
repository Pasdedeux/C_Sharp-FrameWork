
namespace InterfaceKit.Observer
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 发布订阅模式-发布者接口
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// 订阅者池
        /// </summary>
        Dictionary<string, IObserver> ObserverPool
        {
            get;
        }

        /// <summary>
        /// 添加订阅者
        /// </summary>
        /// <param name="observer"></param>
        void AddObserver( IObserver observer );

        /// <summary>
        /// 移除订阅者
        /// </summary>
        /// <param name="observer"></param>
        void RemoveObserver( IObserver observer );

        /// <summary>
        /// 发布者发布通知
        /// </summary>
        /// <param name="subject"></param>
        void Notify( string subject );
    }
}

