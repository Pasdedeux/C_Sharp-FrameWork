using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Subject : ISubject
{
    private static volatile Subject _instance;
    private static object _lock = new object();

    public static Subject Instance
    {
        get
        {
            if (_instance == null)
            {
                lock(_lock)
                {
                    if (_instance == null) 
                        _instance = new Subject();
                }
            }
            return _instance;
        }
    }


    private Dictionary<string, IObserver> dict = new Dictionary<string,IObserver>();

    public Dictionary<string, IObserver> ObserverPool
    {
        get { return dict; }
    }

    private Subject() { }

    public void AddObserver( IObserver observer )
    {
        if( !ObserverPool.ContainsKey( observer.Name ) )
        {
            ObserverPool.Add( observer.Name, observer );
            UnityEngine.Debug.Log( "订阅者【添加】成功,现在订阅者数量"+ObserverPool.Count );
        }
        else
        {
            UnityEngine.Debug.Log( observer.Name + " is already exist" );
        }
    }

    public void RemoveObserver( IObserver observer )
    {
        if( ObserverPool.ContainsKey( observer.Name ) )
        {
            ObserverPool.Remove( observer.Name );
            UnityEngine.Debug.Log( "订阅者【移除】成功,现在订阅者数量" + ObserverPool.Count );
        }
        else
        {
            UnityEngine.Debug.Log( "Couldn't find " + observer.Name + " and no observer rmoved" );
        }

    }

    public void Notify( string subject )
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        //效率会稍微慢点？
        //IEnumerable<IObserver> jquary = from keyValue in ObserverPool
        //                                select keyValue.Value;

        //foreach( IObserver item in jquary )
        //{
        //    item.IUpdate( subject );
        //}



        foreach( var item in ObserverPool )
        {
            item.Value.IUpdate( subject );
        }

        sw.Stop();
        UnityEngine.Debug.Log( sw.ElapsedMilliseconds );
    }
}
