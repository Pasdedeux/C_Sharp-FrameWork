using UnityEngine;
using System.Collections;

public class GetBoxColliderWorldPosition : MonoBehaviour
{
    //8个标志位 ，用来在scene里预览
    public Transform[] points;
    //需要提取Boxcollier顶点的对象
    public BoxCollider cube;


    void Start( )
    {
        //父节只能调节位置， 不能调节旋转和缩放。
        Transform parent = cube.transform.parent;
        while ( parent != null )
        {
            parent.localRotation = Quaternion.Euler( Vector3.zero );
            parent.localScale = Vector3.one;
            parent = parent.parent;
        }
    }

    void Update( )
    {
        Vector3[] veces = GetBoxColliderVertexPositions( cube );
        for ( int i = 0 ; i < veces.Length ; i++ )
        {
            points[ i ].transform.position = veces[ i ];
        }
    }

    Vector3[] GetBoxColliderVertexPositions( BoxCollider boxcollider )
    {
        var vertices = new Vector3[ 8 ];
        //下面4个点
        vertices[ 0 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , -boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 1 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , -boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 2 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , -boxcollider.size.y , -boxcollider.size.z ) * 0.5f );
        vertices[ 3 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , -boxcollider.size.y , -boxcollider.size.z ) * 0.5f );
        //上面4个点
        vertices[ 4 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 5 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 6 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , boxcollider.size.y , -boxcollider.size.z ) * 0.5f );
        vertices[ 7 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , boxcollider.size.y , -boxcollider.size.z ) * 0.5f );

        return vertices;
    }
}
