using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    //需要的反射点数量
    private int _reflectPoint = 2;

    //射入方向/反射方向
    private Vector3 _reflectDirection;

    private Ray _ray;
    private RaycastHit _hit;
    private LineRenderer _lineRenderer;


    void Awake( )
    {
        _lineRenderer = transform.GetComponent<LineRenderer>();
    }

    void Update( )
    {
        //由物体当前位置，朝Z轴发射射线
        _ray = new Ray( transform.position , transform.forward );

        //Scene场景中渲染一条射线线条
        Debug.DrawRay( transform.position , transform.forward * 100 , Color.cyan );

        //Linerender当前包含的点位数量为 反射点数量+加上一个初始位置
        _lineRenderer.numPositions = _reflectPoint+1;

        //设置Linerender中第一个点位为初始位置
        _lineRenderer.SetPosition( 0 , transform.position );

        //有多少点位，就发射多少次射线
        for ( int i = 0 ; i < _reflectPoint ; i++ )
        {
            if ( Physics.Raycast( _ray.origin , _ray.direction , out _hit , 100 ) )
            {
                //将当前射线在碰撞点处根据法线向量反射，得到新的方向向量
                _reflectDirection = Vector3.Reflect( _ray.direction , _hit.normal );

                //从碰撞点处按照新的方向发射射线
                _ray = new Ray( _hit.point , _reflectDirection );

                //Scene场景下渲染射线线条
                Debug.DrawRay( _hit.point , _hit.normal * 3 , Color.blue );
                Debug.DrawRay( _hit.point , _reflectDirection * 100 , Color.cyan );
                Debug.Log( "Object name: " + _hit.transform.name );

                //将下一个点位设置为碰撞点信息，Linerender做联线渲染
                _lineRenderer.SetPosition( i + 1 , _hit.point );
            }
        }
    }
}