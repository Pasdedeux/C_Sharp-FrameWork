using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( MeshFilter ) , typeof( MeshRenderer ) )]
public class L_Cube : MonoBehaviour
{
    private float xLength = 2, yLength = 2, zLength = 2;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int xSize = 2, ySize = 2, zSize = 2;

    //每个顶点的实际间隔距离
    private float _xUnit = 1, _yUnit = 1, _zUnit = 1;

    private void Awake( )
    {
       //应该写点什么，先放着吧。。。
    }

    public void Generate( float xLength, float yLength, float zLength )
    {
        this.xLength = xLength;
        this.yLength = yLength;
        this.zLength = zLength;

        _xUnit = xLength / xSize;
        _yUnit = yLength / ySize;
        _zUnit = zLength / zSize;

        GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
        _mesh.name = "Procedural L_Cube";

        CreateVertices();
        CreateTriangles();

        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
    }

    /// <summary>
    /// 三角形的数量简单的等于组合的六个面的数量
    /// </summary>
    private void CreateTriangles( )
    {
        //接下来两行需要着重理解:代表面数及三角形数量
        int quads = ( xSize * ySize + xSize * zSize + ySize * zSize ) * 2;
        int[] triangles = new int[ quads * 6 ];
        int ring = ( xSize + zSize ) * 2;
        int t = 0, v = 0;

        for ( int y = 0 ; y < ySize ; y++, v++ )
        {
            for ( int q = 0 ; q < ring - 1 ; q++, v++ )
            {
                t = SetQuad( triangles , t , v , v + 1 , v + ring , v + ring + 1 );
            }
            //每一圈最后一个三角形第二、四点需要回复到起点的两个点位上去
            t = SetQuad( triangles , t , v , v - ring + 1 , v + ring , v + 1 );
        }

        t = CreateTopFace( triangles , t , ring );
        t = CreateBottomFace( triangles , t , ring );

        _mesh.triangles = triangles;
    }

    private int CreateBottomFace( int[] triangles , int t , int ring )
    {
        int v = 1;
        int vMid = _vertices.Length - ( xSize - 1 ) * ( zSize - 1 );
        t = SetQuad( triangles , t , ring - 1 , vMid , 0 , 1 );
        for ( int x = 1 ; x < xSize - 1 ; x++, v++, vMid++ )
        {
            t = SetQuad( triangles , t , vMid , vMid + 1 , v , v + 1 );
        }
        t = SetQuad( triangles , t , vMid , v + 2 , v , v + 1 );

        int vMin = ring - 1;
        int vMax = v + 2;
        vMid -= xSize - 2;

        for ( int z = 1 ; z < zSize - 1 ; z++, vMin--, vMid++, vMax++ )
        {
            t = SetQuad( triangles , t , vMin , vMid + xSize - 1 , vMin + 1 , vMid );
            for ( int x = 1 ; x < xSize - 1 ; x++, vMid++ )
            {
                t = SetQuad( triangles , t , vMid + xSize - 1 , vMid + xSize , vMid , vMid + 1 );
            }
            t = SetQuad( triangles , t , vMid + xSize - 1 , vMax + 1 , vMid , vMax );
        }

        int vTop = vMin - 1;
        t = SetQuad( triangles , t , vTop + 1 , vTop , vTop + 2 , vMid );
        for ( int x = 1 ; x < xSize - 1 ; x++, vTop--, vMid++ )
        {
            t = SetQuad( triangles , t , vTop , vTop - 1 , vMid , vMid + 1 );
        }
        t = SetQuad( triangles , t , vTop , vTop - 1 , vMid , vTop - 2 );
        return t;
    }

    private int CreateTopFace( int[] triangles , int t , int ring )
    {
        int v = ring * ySize;
        for ( int x = 0 ; x < xSize - 1 ; x++, v++ )
        {
            t = SetQuad( triangles , t , v , v + 1 , v + ring - 1 , v + ring );
        }
        t = SetQuad( triangles , t , v , v + 1 , v + ring - 1 , v + 2 );

        //跟踪行的最小索引点
        int vMin = ring * ( ySize + 1 ) - 1;
        int vMid = vMin + 1;
        //行的最后四分之一再次处理外圈，所以追踪最大顶点
        int vMax = v + 2;

        for ( int z = 1 ; z < zSize - 1 ; z++, vMin--, vMid++, vMax++ )
        {
            //行的最小顶点索引处
            t = SetQuad( triangles , t , vMin , vMid , vMin - 1 , vMid + xSize - 1 );
            //行的中间部分
            for ( int x = 1 ; x < xSize - 1 ; x++, vMid++ )
            {
                t = SetQuad( triangles , t , vMid , vMid + 1 , vMid + xSize - 1 , vMid + xSize );
            }
            //行的最大顶点索引处
            t = SetQuad( triangles , t , vMid , vMax , vMid + xSize - 1 , vMax + 1 );
        }

        //最后一行第一个四边形
        int vTop = vMin - 2;
        t = SetQuad( triangles , t , vMin , vMid , vTop + 1 , vTop );
        //循环通过行中间
        for ( int x = 1 ; x < xSize - 1 ; x++, vTop--, vMid++ )
        {
            t = SetQuad( triangles , t , vMid , vMid + 1 , vTop , vTop - 1 );
        }
        //最后四分之一
        t = SetQuad( triangles , t , vMid , vTop - 2 , vTop , vTop - 1 );

        return t;
    }

    private void CreateVertices( )
    {
        //立方体所有顶点不重复，所以计算顶点分三类：
        //1、八个角顶点
        int cornerVertices = 8;
        //2、十二条边线：等于四组 X Y Z边， 每个边点数等于相应size-1（去除两端角顶点）
        int edgeVertices = ( xSize + ySize + zSize - 3 ) * 4;
        //3、面内顶点:（去除两端边线顶点）
        int faceVertices = (
            ( xSize - 1 ) * ( ySize - 1 ) +
            ( xSize - 1 ) * ( zSize - 1 ) +
            ( ySize - 1 ) * ( zSize - 1 )
            ) * 2;
        _vertices = new Vector3[ cornerVertices + edgeVertices + faceVertices ];

        //以Y周为一层，逐层叠加
        int v = 0;
        for ( int y = 0 ; y <= ySize ; y++ )
        {
            for ( int x = 0 ; x <= xSize ; x++ )
            {
                _vertices[ v++ ] = new Vector3( x * _xUnit , y * _yUnit , 0 * _zUnit );
            }

            for ( int z = 1 ; z <= zSize ; z++ )//不用z==0 因为起点已经有X
            {
                _vertices[ v++ ] = new Vector3( xSize * _xUnit , y * _yUnit , z * _zUnit );
            }

            for ( int x = xSize - 1 ; x >= 0 ; x-- )
            {
                _vertices[ v++ ] = new Vector3( x * _xUnit , y * _yUnit , zSize * _zUnit );
            }

            for ( int z = zSize - 1 ; z > 0 ; z-- )//不用z>=0 因为起点已经有X
            {
                _vertices[ v++ ] = new Vector3( 0 * _xUnit , y * _yUnit , z * _zUnit );
            }
        }
        //上下盖子中间面部分点
        for ( int x = 1 ; x < xSize ; x++ )
        {
            for ( int z = 1 ; z < zSize ; z++ )
            {
                _vertices[ v++ ] = new Vector3( x * _xUnit , ySize * _yUnit , z * _zUnit );
            }
        }

        for ( int x = 1 ; x < xSize ; x++ )
        {
            for ( int z = 1 ; z < zSize ; z++ )
            {
                _vertices[ v++ ] = new Vector3( x * _xUnit , 0 * _yUnit , z * _zUnit );
            }
        }

        _mesh.vertices = _vertices;
    }

    //private void OnDrawGizmos( )
    //{
    //    if ( _vertices == null ) return;

    //    Gizmos.color = Color.black;
    //    for ( int i = 0 ; i < _vertices.Length ; i++ )
    //    {
    //        Gizmos.DrawSphere( _vertices[ i ] , 0.1f );
    //    }
    //}

    /// <summary>
    /// 将创建一个方形面合成一个方法。（顺时针排布：如 0-1-2，2-1-3,左下角为0，右上角为3）
    /// </summary>
    /// <param name="triangles">提供的空的三角形数组</param>
    /// <param name="i">增量索引</param>
    /// <param name="v00">0点</param>
    /// <param name="v10">2点</param>
    /// <param name="v01">1点</param>
    /// <param name="v11">3点</param>
    /// <returns>下一个索引起点</returns>
    private static int SetQuad( int[] triangles , int i , int v00 , int v10 , int v01 , int v11 )
    {
        //对于一个面的两个三角形的六个点来说，有以下关系
        triangles[ i ] = v00;
        triangles[ i + 1 ] = triangles[ i + 4 ] = v01;
        triangles[ i + 2 ] = triangles[ i + 3 ] = v10;
        triangles[ i + 5 ] = v11;
        return i + 6;
    }
}
