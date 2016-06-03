using UnityEngine;
using System.Collections;

public class CubeIdle : FSMState<CubeAI> 
{
    int idleTime = 0;
    float deltaTime = 0;
    int i = 0;

    public override void Enter()
    {
        base.Enter();
        Debug.Log( "Cube Idle enter" );
        i = 0;
        deltaTime = 0;
        idleTime = Random.Range( 8, 16 );
    }

    public override void Excute()
    {
        base.Excute();
        i++;
        Debug.Log( "Cube Idle Excuted ==>"+i );
        deltaTime += Time.deltaTime;
        if( deltaTime >= idleTime )
        {
            Target.ChangeAIState( CubeState.Run );
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log( "Cube Idle Exit..." );
        i = 0;
    }
	
}
