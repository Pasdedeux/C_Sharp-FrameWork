using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 池对象，用于缓存一类特定对象Object
/// </summary>
public class Pool
{
    //本池关联的预制件
    private GameObject _prefabTp;
    //池队列
    private Queue<GameObject> _queue;

    public Pool( GameObject poolObj , int maxNum )
    {
        //保存预制件引用
        _prefabTp = poolObj;
        //初始化队列容量
        _queue = new Queue<GameObject>( maxNum );
        //按照容量初始化指定数量的预制件，容量估计尽量准确，增长倍数默认为2
        for ( int i = 0 ; i < maxNum ; i++ )
        {
            GameObject go = GameObject.Instantiate( poolObj );
            Recycle( go );
        }
    }

    /// <summary>
    /// 向池中回收一个对象
    /// </summary>
    /// <param name="poolObject"></param>
    public void Recycle( GameObject poolObject )
    {
        //回收前将对象按照需求置为“默认状态”
        poolObject.GetComponent<BasePoolObject>().InitStatus();
        //避免阻止InitStatus被重写时产生阻碍
        poolObject.SetActive( false );
        //加入队尾
        _queue.Enqueue( poolObject );
    }

    /// <summary>
    /// 从池中获取一个对象并从池中移除，如果池已空，则new一个新对象使用
    /// </summary>
    public GameObject Get( )
    {
        GameObject result;
        if ( _queue.Count > 0 ) result = _queue.Dequeue();      //从队列头取出并激活
        else result = GameObject.Instantiate( _prefabTp );      //如果为空池，则直接new一个

        result.SetActive( true );
        return result;
    }
}
