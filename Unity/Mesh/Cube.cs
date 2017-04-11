using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof( MeshRenderer ))]
public class Cube : MonoBehaviour {

    public int xSize, ySize, zSize;

    private Mesh _mesh;
    private Vector3[] _vertices;

    private void Awake( )
    {
        StartCoroutine( Generate() );

    }

    private IEnumerator Generate( )
    {
        GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
        _mesh.name = "Procedural Cube";
        WaitForSeconds wait = new WaitForSeconds( 0.05f );

        //计算顶点分三类：
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

        //以Y周为一环，逐层叠加
        int v = 0;
        for ( int y = 0 ; y <= ySize ; y++ )
        {
            for ( int x = 0 ; x <= xSize ; x++ )
            {
                _vertices[ v++ ] = new Vector3( x , y , 0 );
                yield return wait;
            }

            for ( int z = 1 ; z <= zSize ; z++ )//不用z==0 因为起点已经有X
            {
                _vertices[ v++ ] = new Vector3( xSize , y , z );
                yield return wait;
            }

            for ( int x = xSize - 1 ; x >= 0 ; x-- )
            {
                _vertices[ v++ ] = new Vector3( x , y , zSize );
                yield return wait;
            }

            for ( int z = zSize - 1 ; z > 0 ; z-- )//不用z>=0 因为起点已经有X
            {
                _vertices[ v++ ] = new Vector3( 0 , y , z );
                yield return wait;
            }
            yield return wait;
        }

        yield return wait;

        //上下盖子中间面部分点
        for ( int x = 1 ; x < xSize ; x++ )
        {
            for ( int z = 1 ; z < zSize ; z++ )
            {
                _vertices[ v++ ] = new Vector3( x , ySize , z );
                yield return wait;
            }
        }

        for ( int x = 1 ; x < xSize ; x++ )
        {
            for ( int z = 1 ; z < zSize ; z++ )
            {
                _vertices[ v++ ] = new Vector3( x , 0 , z );
                yield return wait;
            }
        }
    }

    private void OnDrawGizmos( )
    {
        if ( _vertices == null ) return;

        Gizmos.color = Color.black;
        for ( int i = 0 ; i < _vertices.Length ; i++ )
        {
            Gizmos.DrawSphere( _vertices[ i ] , 0.1f );
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
