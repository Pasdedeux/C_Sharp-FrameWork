using UnityEngine;
using System.Collections;

/// <summary>
/// 有限状态机状态基类
/// </summary>
/// <typeparam name="Entity_type">
/// 泛型类
/// </typeparam>
public class FSMState<Entity_type> 
{
    public Entity_type Target;

    public virtual void Enter()
    {
        Debug.Log( this.ToString() + "  Enter" );
    }


    /// <summary>
    /// 执行状态的逻辑
    /// </summary>
    public virtual void Excute()
    {

    }

    /// <summary>
    /// 退出状态的逻辑
    /// </summary>
    public virtual void Exit()
    {
        Debug.Log( this.ToString() + "   Exit" );
    }

}
