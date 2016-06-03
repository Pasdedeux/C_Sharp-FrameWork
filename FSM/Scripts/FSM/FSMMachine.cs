using UnityEngine;
using System.Collections;
/// <summary>
/// 状态机
/// 使用这个来驱动状态
/// </summary>
/// <typeparam name="Entity_type"></typeparam>
public class FSMMachine<Entity_type>
{
    //状态持有对象
    private Entity_type s_Owner;

    //
    private FSMState<Entity_type> s_CurrentState;
    private FSMState<Entity_type> s_PreviousState;
    private FSMState<Entity_type> s_GlobalState;
    
    /// <summary>
    /// 状态机构造函数
    /// </summary>
    /// <param name="owner">
    /// 状态机的持有对象 CubeAI
    /// </param>
    public FSMMachine(Entity_type owner)
    {
        s_Owner = owner;
        s_CurrentState = null;
        s_GlobalState = null;
        s_PreviousState = null;
    }

    /// <summary>
    /// 设置当前状态
    /// </summary>
    /// <param name="currentState"></param>
    public void SetCurrentState(FSMState<Entity_type> currentState)
    {
        if( currentState != null )
        {
            //保存当前状态
            s_CurrentState = currentState;
            //设置状态中的 Target
            s_CurrentState.Target = s_Owner;
            s_CurrentState.Enter();
        }
    }

    /// <summary>
    /// 设置全局状态
    /// 运行全局状态
    /// </summary>
    /// <param name="globalState"></param>
    public void SetGlobalState( FSMState<Entity_type> globalState)
    {
        if( globalState != null )
        {
            s_GlobalState = globalState;
            s_GlobalState.Target = s_Owner;
            s_GlobalState.Enter();
        }
        else
        {
            Debug.LogError( "不能设置为空状态" );
        }
    }

    /// <summary>
    /// 进入全局状态
    /// </summary>
    public void GlobalStateEnter()
    {
        
    }

    /// <summary>
    /// Update 方法,被外部Update调用
    /// </summary>
    public void FSMUpdate()
    {
        if( s_GlobalState != null )
        {
            s_GlobalState.Excute();
        }
        if( s_CurrentState != null )
        {
            s_CurrentState.Excute();
        }
    }


    public void ChangeState( FSMState<Entity_type> newState )
    {
        if(newState==null)
        {
            Debug.LogError( "不能使用空的状态" );
        }

        //退出当前状态
        s_CurrentState.Exit();

        //存储为过去状态
        s_PreviousState = s_CurrentState;

        //设置新状态为当前状态
        s_CurrentState = newState;

        //将新状态的使用对象设为当前
        s_CurrentState.Target = s_Owner;

        //转换进入当前状态
        s_CurrentState.Enter();

    }

    /// <summary>
    /// 切换回上一个阶段
    /// </summary>
    public void RevertToPreviousState()
    {
        this.ChangeState( s_PreviousState );
    }

    /// <summary>
    /// 获取当前状态
    /// </summary>
    /// <returns></returns>
    public FSMState<Entity_type> GetCurrentState()
    {
        return s_CurrentState;
    }

    /// <summary>
    /// 过去当前全局状态
    /// </summary>
    /// <returns></returns>
	public FSMState<Entity_type> GetGlobalState()
    {
        return s_GlobalState;
    }
}
