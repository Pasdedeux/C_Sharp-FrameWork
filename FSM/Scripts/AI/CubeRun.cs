using UnityEngine;
using System.Collections;

public class CubeRun : FSMState<CubeAI> 
{
    int runTime = 0;
    float deltaTime = 0;

    public override void Enter()
    {
        base.Enter();

        Debug.Log( "CubeRun Entered..." );
        runTime = Random.Range( 5, 10 );
        deltaTime = 0;
    }

    public override void Excute()
    {
        base.Excute();
        Target.transform.Translate( Vector3.forward * 1 * Time.deltaTime );

        Debug.Log( "CubeRun 移动到了" + Target.transform.position );
        deltaTime += Time.deltaTime;
        if(deltaTime>=runTime)
        {
            Target.ChangeAIState( CubeState.Idle );
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log( "CubeRun 跑累了" );
    }

}
