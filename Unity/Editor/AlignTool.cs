using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AlignTool : EditorWindow
{
    #region 场景物件对齐工具
    [MenuItem( "Plugins/对齐工具" )]
    public static void AlignWindow( )
    {
        var windowSize = new Rect( 0 , 0 , 300 , 400 );
        var window = ( AlignTool ) GetWindowWithRect( typeof( AlignTool ) , windowSize , true , "对齐工具" );
        window.Show();
    }
    #endregion

    private Dictionary<GameObject , Vector3> _lastLocs = new Dictionary<GameObject , Vector3>();

    private void OnGUI( )
    {
        EditorGUILayout.BeginVertical();

        if ( GUI.RepeatButton( new Rect( 0 , 0 , 300 , 80 ) , "参照对象" ) )
        {
            var array = Selection.gameObjects;

            for ( int i = 0 ; i < array.Length ; i++ )
            {
                if ( i > 0 )
                {
                    bool active = array[ i ].GetComponent<MeshRenderer>().enabled;
                    array[ i ].GetComponent<MeshRenderer>().enabled = active ? false : true;
                }
            }
        }

        if ( GUI.Button( new Rect( 0 , 100 , 300 , 80 ) , "重置" ) )
        {
            foreach ( var item in _lastLocs )
            {
                item.Key.transform.localPosition = item.Value;
            }
        }

        GUI.BeginGroup( new Rect( 0 , 200 , 300 , 500 ) );

        int baseWidth = 140;
        int baseHeight = 80;
        if ( GUI.Button( new Rect( 0 , 0 , baseWidth , baseHeight ) , " 蓝色正向对齐 " ) )
        {
            RecordPosition();

            var array = Selection.gameObjects;
            var standard = array[ 0 ];
            var standardrulerPoint = GetRulerPoint( standard , new Vector3( 1 , 0 , 1 ) );

            for ( int i = 0 ; i < array.Length ; i++ )
            {
                if ( i > 0 )
                {
                    var item = array[ i ];
                    var rulerPoint = GetRulerPoint( item , new Vector3( 1 , 0 , 1 ) );
                    var diff = standardrulerPoint.z - rulerPoint.z;

                    item.transform.position = new Vector3( item.transform.position.x , item.transform.position.y , item.transform.position.z + diff );
                }
            }

        }
        if ( GUI.Button( new Rect( baseWidth + 10 , 0 , baseWidth , baseHeight ) , " 蓝色负向对齐 " ) )
        {
            RecordPosition();

            var array = Selection.gameObjects;
            var standard = array[ 0 ];
            var standardrulerPoint = GetRulerPoint( standard , new Vector3( 1 , 0 , -1 ) );

            for ( int i = 0 ; i < array.Length ; i++ )
            {
                if ( i > 0 )
                {
                    var item = array[ i ];
                    var rulerPoint = GetRulerPoint( item , new Vector3( 1 , 0 , -1 ) );
                    var diff = standardrulerPoint.z - rulerPoint.z;

                    item.transform.position = new Vector3( item.transform.position.x , item.transform.position.y , item.transform.position.z + diff );
                }
            }

        }
        if ( GUI.Button( new Rect( 0 , baseHeight + 5 , baseWidth , baseHeight ) , " 红色正向对齐 " ) )
        {
            RecordPosition();

            var array = Selection.gameObjects;
            var standard = array[ 0 ];
            var standardrulerPoint = GetRulerPoint( standard , new Vector3( 1 , 0 , 1 ) );

            for ( int i = 0 ; i < array.Length ; i++ )
            {
                if ( i > 0 )
                {
                    var item = array[ i ];
                    var rulerPoint = GetRulerPoint( item , new Vector3( 1 , 0 , 1 ) );
                    var diff = standardrulerPoint.x - rulerPoint.x;

                    item.transform.position = new Vector3( item.transform.position.x + diff , item.transform.position.y , item.transform.position.z );
                }
            }
        }
        if ( GUI.Button( new Rect( baseWidth + 10 , baseHeight + 5 , baseWidth , baseHeight ) , " 红色负向对齐 " ) )
        {
            RecordPosition();

            var array = Selection.gameObjects;
            var standard = array[ 0 ];
            var standardrulerPoint = GetRulerPoint( standard , new Vector3( -1 , 0 , 1 ) );

            for ( int i = 0 ; i < array.Length ; i++ )
            {
                if ( i > 0 )
                {
                    var item = array[ i ];
                    var rulerPoint = GetRulerPoint( item , new Vector3( -1 , 0 , 1 ) );
                    var diff = standardrulerPoint.x - rulerPoint.x;

                    item.transform.position = new Vector3( item.transform.position.x + diff , item.transform.position.y , item.transform.position.z );
                }
            }
        }

        GUI.EndGroup();
    }

    Vector3[] GetBoxColliderVertexPositions( BoxCollider boxcollider )
    {
        var vertices = new Vector3[ 8 ];
        vertices[ 0 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , -boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 1 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , -boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 2 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , -boxcollider.size.y , -boxcollider.size.z ) * 0.5f );
        vertices[ 3 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , -boxcollider.size.y , -boxcollider.size.z ) * 0.5f );

        vertices[ 4 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 5 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , boxcollider.size.y , boxcollider.size.z ) * 0.5f );
        vertices[ 6 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( -boxcollider.size.x , boxcollider.size.y , -boxcollider.size.z ) * 0.5f );
        vertices[ 7 ] = boxcollider.transform.TransformPoint( boxcollider.center + new Vector3( boxcollider.size.x , boxcollider.size.y , -boxcollider.size.z ) * 0.5f );
        return vertices;
    }

    Vector3 GetRulerPoint( GameObject item , Vector3 direction )
    {
        var verticals = GetBoxColliderVertexPositions( item.transform.GetComponent<BoxCollider>() );

        float sX = direction.x > 0 ? verticals.Max( delegate ( Vector3 e ) { return e.x; } ) : verticals.Min( delegate ( Vector3 e ) { return e.x; } );
        float sZ = direction.z > 0 ? verticals.Max( delegate ( Vector3 e ) { return e.z; } ) : verticals.Min( delegate ( Vector3 e ) { return e.z; } );
        float minY = verticals.Min( delegate ( Vector3 e ) { return e.y; } );

        //未知原因，美术提交的素材获取碰撞盒数据会出现无法筛选出指定坐标点的问题，所以这里用一个区间来筛选出目标点，如果碰撞点足够小，可能会带来选择点的误差
        var rulerPoint = verticals.Where( delegate ( Vector3 e )
        {
            return
            ( e.x >= sX - 0.1f && e.x <= sX + 0.1f )
            && ( e.y >= minY - 0.1f && e.y <= minY + 0.1f )
            && e.z >= sZ - 0.1f && e.z <= sZ + 0.1f;
        } ).ToList<Vector3>();

        //var test1 = verticals.Where( delegate ( Vector3 e ) { return e.x >= sX - 0.1f && e.x <= sX + 0.1f; } ).ToArray();
        //var test2 = test1.Where( delegate ( Vector3 e ) { return e.y >= minY - 0.1f && e.y <= minY + 0.1f; } ).ToArray();
        //var test3 = test2.Where( delegate ( Vector3 e ) { return e.z >= sZ - 0.1f && e.z <= sZ + 0.1f; } ).ToArray();

        if ( rulerPoint.Count == 0 )
        {
            Debug.LogError( item.name + " 无法对齐 " );
        }
        return rulerPoint[ 0 ];
    }

    void RecordPosition( )
    {
        _lastLocs.Clear();
        var array = Selection.gameObjects;
        for ( int i = 0 ; i < array.Length ; i++ )
        {
            _lastLocs.Add( array[ i ] , array[ i ].transform.localPosition );
        }
    }
}
