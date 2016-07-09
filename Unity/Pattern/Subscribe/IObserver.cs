
namespace InterfaceKit.Observer
{
    using System.Collections;

    /// <summary>
    /// 发布订阅模式-订阅者
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 订阅者名字Key
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 订阅者的通知响应
        /// </summary>
        /// <param name="subject"></param>
        void IUpdate( string subject );
    }
}

