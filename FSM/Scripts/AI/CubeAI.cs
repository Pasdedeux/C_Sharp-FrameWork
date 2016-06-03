using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//状态枚举
public enum CubeState
{
    Run,
    Idle
}

public class CubeAI : MonoBehaviour {

    //状态机对象，用于状态管理
    FSMMachine<CubeAI> m_Machine;
    //存放状态映射
    public Dictionary<CubeState, FSMState<CubeAI>> stateDic;
    
    void Awake()
    {
        stateDic = new Dictionary<CubeState, FSMState<CubeAI>>();

        //加入状态对象对
        stateDic.Add( CubeState.Run, new CubeRun() );
        stateDic.Add( CubeState.Idle, new CubeIdle() );
    }

	// Use this for initialization
	void Start () 
    {
        //起始状态为站立状态
        m_Machine = new FSMMachine<CubeAI>( this );
        m_Machine.SetCurrentState( stateDic[CubeState.Idle] );
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_Machine.FSMUpdate();
	}

    public void ChangeAIState(CubeState state)
    {
        m_Machine.ChangeState(stateDic[state]);
    }
}
