using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubes : MonoBehaviour 
{
    public GameObject prefabs;
    public int xNum, yNum, zNum;

	void Start () 
	{
      
	}

    void BeginSplit()
    {
        GameObject target = GameObject.Find( "target" );
        Vector3 size = transform.TransformVector( target.GetComponent<BoxCollider>().size );
        float xlength = size.x / xNum;
        float ylength = size.y / yNum;
        float zlength = size.z / zNum;
        target.GetComponent<BoxCollider>().enabled = false;

        for ( int y = 0 ; y < yNum ; y++ )
        {
            for ( int x = 0 ; x < xNum ; x++ )
            {
                for ( int z = 0 ; z < yNum ; z++ )
                {
                    GameObject obj = Instantiate( prefabs );
                    var sc = obj.GetComponent<L_Cube>();
                    sc.Generate( xlength , ylength , zlength );
                    obj.transform.position = new Vector3( x * xlength , y * ylength , z * zlength ) + target.transform.position + new Vector3( -0.5f , -0.5f , -0.5f );
                    obj.AddComponent<BoxCollider>().size *= 2f;
                    obj.AddComponent<Rigidbody>().useGravity = true;
                }
            }
        }
    }

    private void OnGUI( )
    {
        if( GUI.Button( new Rect( 100,100,150,50 ),"split" ))
        {
            BeginSplit();
        }
    }
}
