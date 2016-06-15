//*****************************
//  IUnitySingleton
//
//  Created by Derek Liu
//
//*****************************

using UnityEngine;
using System.Collections;

/// <summary>
/// 单例类泛型基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
public class IUnitySingleton<T> : MonoBehaviour where T: Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            //是否已存在单例挂载对象
            if ( _instance == null )
            {
                _instance = FindObjectOfType( typeof(T) ) as T;

                //若不存在则创建一个【隐匿对象】将（继承类）以组件方式挂载
                if ( _instance == null )
                {
                    GameObject gObj = new GameObject();
                    gObj.name = "Singleton(" + typeof( T ).Name + ")";
                    gObj.hideFlags = HideFlags.HideAndDontSave;//可见性，以及不可消除性

                    //这里会先触发继承类Awake()方法
                    _instance = gObj.AddComponent( typeof( T ) ) as T;
                }
            }
            
            return _instance;
        }
    }

    /// <summary>
    /// 泛型Awake基类-移除
    /// </summary>
    public virtual void onDestroy( )
    {
        if ( _instance != null )
        {
            _instance = null;
        }
    }
}
