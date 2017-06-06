using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 池类型
/// </summary>
public enum PoolType
{
    /// <summary>
    /// 物体1类型池
    /// </summary>
    GameItem1,
    /// <summary>
    /// 物体2类型池
    /// </summary>
    GameItem2,
}


/// <summary>
/// 对象池管理器
/// </summary>
public class PoolsManager : Singleton<PoolsManager> 
{
    //不同预制件生成不同对象池，并加入池管理
    public GameObject[] prefabList;
    //对象池字典，管理全部对象池
    private Dictionary<PoolType , Pool> _poolsManager;

    void Awake()
    {
        //以下这一部分建立对象池集合的过程可以用配置完成，更灵活
        _poolsManager = new Dictionary<PoolType , Pool>();
        //链接预制件和对象池，同时初始化池大小
        _poolsManager.Add( PoolType.GameItem1 , new Pool( prefabList[ ( int ) PoolType.GameItem1 ] , 10 ) );
        _poolsManager.Add( PoolType.GameItem2 , new Pool( prefabList[ ( int ) PoolType.GameItem2 ] , 5 ) );
    }

    
    /// <summary>
    /// 回收一个对象
    /// 
    /// 对于已配置池类型或池类型存在的对象，被池回收，否则被销毁
    /// </summary>
    /// <param name="obj"></param>
    public void Recycle( GameObject obj )
    {
        if( obj == null )
        {
            Debug.LogError( "被回收的对象为无效" );
            return;
        }

        BasePoolObject po = obj.GetComponent<BasePoolObject>();
        if(po == null)
        {
            Debug.LogError( "被回收的对象并非被定义的池中对象" );
            return;
        }

        //根据归属的池回收到对应池中
        if( _poolsManager.ContainsKey( po.poolType ) )
            _poolsManager[ po.poolType ].Recycle( obj );
        else
            Destroy( obj.gameObject );
    }

    /// <summary>
    /// 从指定池中获取一个对象
    /// </summary>
    /// <param name="objType"></param>
    public GameObject Alloc( PoolType objType )
    {
        GameObject result = null;
        if( _poolsManager.ContainsKey( objType ) )
            result = _poolsManager[ objType ].Get();
        else
            Debug.LogError( "不存在池，无法获取指定对象" );
        return result;
    }
}
