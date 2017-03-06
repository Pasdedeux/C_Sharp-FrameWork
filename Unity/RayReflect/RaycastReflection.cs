using UnityEngine;
using System.Collections;

[RequireComponent( typeof( LineRenderer ) )]
public class RaycastReflection : MonoBehaviour
{
    //添加的line renderer组件
    private LineRenderer _lineRenderer;

    //一条射线
    private Ray _ray;

    //一个RaycastHit类型的变量, 可以得到射线碰撞点的信息
    private RaycastHit _hit;

    //反射的方向
    private Vector3 _inDirection;

    //反射次数
    public int nReflections = 1;

    //line renderer组件上点的数量
    private int _reflectPoint;


    void Awake( )
    {
        _lineRenderer = transform.GetComponent<LineRenderer>();
    }


    void Update( )
    {
        //z轴方向
        _ray = new Ray( transform.position , transform.forward );

        //只有在编辑器的Scene窗口才会看到的射线，用于调试
        Debug.DrawRay( transform.position , transform.forward * 100 , Color.magenta );

        //将lineRenderer的点数设置成和反射次数相等
        _reflectPoint = nReflections;

        //使lineRenderer有nPoints个点
        _lineRenderer.numPositions = _reflectPoint;

        //将lineRenderer的第一个点设置为当前物体的位置
        _lineRenderer.SetPosition( 0 , transform.position );

        for ( int i = 0 ; i <= nReflections ; i++ )
        {
            //当射线还没有反射的时候
            if ( i == 0 )
            {
                //检查射线是否碰到了墙壁
                if ( Physics.Raycast( _ray.origin , _ray.direction , out _hit , 100 ) )
                {
                    //反射方向就是当前碰撞点的反射角
                    _inDirection = Vector3.Reflect( _hit.point , _hit.normal );

                    //新建一条射线, 用刚才的碰撞点当做新射线的初始点，用反射方向当做他的发射方向 
                    _ray = new Ray( _hit.point , _inDirection );

                    //调试用信息，绘制法线、射线
                    Debug.DrawRay( _hit.point , _hit.normal * 3 , Color.blue );

                    Debug.DrawRay( _hit.point , _inDirection * 100 , Color.magenta );

                    //打印被射线击中物体的名称
                    Debug.Log( "Object name: " + _hit.transform.name );

                    //如果反射次数为1
                    if ( nReflections == 1 )
                    {
                        //给lineRenderer上新加一个点
                        _lineRenderer.numPositions = ++_reflectPoint;
                    }

                    //将lineRenderer的下一个点的位置设置为击中点的位置  
                    _lineRenderer.SetPosition( i + 1 , _hit.point );

                }
            }
            else // 如果射线已经反射过至少一次
            {
                //检查射线是否碰到了墙壁
                if ( Physics.Raycast( _ray.origin , _ray.direction , out _hit , 100 ) )//发射一条100个单位长的射线
                {
                    //反射方向就是当前碰撞点的反射角
                    _inDirection = Vector3.Reflect( _inDirection , _hit.normal );

                    //新建一条射线, 用刚才的碰撞点当做新射线的初始点，用反射方向当做他的发射方向  
                    _ray = new Ray( _hit.point , _inDirection );

                    //调试用信息，绘制法线、射线
                    Debug.DrawRay( _hit.point , _hit.normal * 3 , Color.blue );

                    Debug.DrawRay( _hit.point , _inDirection * 100 , Color.magenta );

                    Debug.Log( "Object name: " + _hit.transform.name );


                    //给lineRenderer上新加一个点
                    _lineRenderer.numPositions = ++_reflectPoint;

                    //将lineRenderer的下一个点的位置设置为击中点的位置  
                    _lineRenderer.SetPosition( i + 1 , _hit.point );

                }
            }
        }
    }
}