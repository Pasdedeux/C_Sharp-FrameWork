using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Observer : MonoBehaviour, IObserver 
{
    private string _name;
    public string Name
    {
        get
        {
            return _name ?? "Key";
        }
        set
        {
            _name = value;
        }
    }

    public void IUpdate( string subject )
    {
        Debug.Log( "Subject is : " + subject );
    }
}
