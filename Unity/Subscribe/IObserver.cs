using System.Collections;

/// <summary>
/// 发布订阅模式-订阅者
/// </summary>
public interface IObserver
{

    string Name { get; set; }
    
    /// <summary>
    /// 订阅者的通知响应
    /// </summary>
    /// <param name="subject"></param>
    void IUpdate( string subject );
}
