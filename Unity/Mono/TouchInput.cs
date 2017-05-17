using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TouchDirection
{
    Unkown,
    Left,
    Right,
    Up,
    Down
}

public class TouchInput : Singleton<TouchInput> 
{
    private Vector2 touchBeginPos;
    private Vector2 touchEndPos;

    void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    public TouchDirection GetTouchMoveDirection( )
    {
        if ( Input.touchCount > 0 )
        {
            TouchDirection dir = TouchDirection.Unkown;
            if ( Input.touches[ 0 ].phase != TouchPhase.Canceled )
            {
                switch ( Input.touches[ 0 ].phase )
                {
                    case TouchPhase.Began:
                        touchBeginPos = Input.touches[ 0 ].position;
                        break;
                    case TouchPhase.Ended:
                        touchEndPos = Input.touches[ 0 ].position;

                        if ( Mathf.Abs( touchBeginPos.x - touchEndPos.x ) > Mathf.Abs( touchBeginPos.y - touchEndPos.y ) )
                        {
                            if ( touchBeginPos.x > touchEndPos.x )
                            {
                                dir = TouchDirection.Left;
                            }
                            else
                            {
                                dir = TouchDirection.Right;
                            }
                        }
                        else
                        {
                            if ( touchBeginPos.y > touchEndPos.y )
                            {
                                dir = TouchDirection.Down;
                            }
                            else
                            {
                                dir = TouchDirection.Up;
                            }
                        }
                        break;
                }
            }
            return dir;
        }
        else
        {
            return TouchDirection.Unkown;
        }
    }
}
