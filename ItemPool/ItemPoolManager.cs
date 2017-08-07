using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPoolManager 
{
    /// <summary>
    /// 超时时限
    /// </summary>
    public const int EXPIRE_TIME = 1 * 60;

    private static Dictionary<string, ItemPool> itemList;

    /// <summary>
    /// 添加一条数据
    /// </summary>
    /// <param name="path"></param>
    public static void PushData(string path)
    {
        if( itemList == null ) itemList = new Dictionary<string, ItemPool>();

        if( !itemList.ContainsKey( path ) ) itemList.Add( path, new ItemPool( path ) );
    }

    /// <summary>
    /// 添加 GameObjdect 对象
    /// </summary>
    /// <param name="path"></param>
    /// <param name="gameObj"></param>
    public static void PushObject(string path, GameObject gameObj)
    {
        if( itemList == null || !itemList.ContainsKey( path ) ) PushData( path );
        //添加此路径下对象
        itemList[path].PushItem( gameObj );
    }

    /// <summary>
    /// 清除PoolManager字典一条数据
    /// </summary>
    /// <param name="path"></param>
    public static void RemoveData(string path)
    {
        if( itemList == null ) return;
        itemList.Remove( path );
    }

    /// <summary>
    /// 移除指定对象
    /// </summary>
    /// <param name="path"></param>
    /// <param name="gameObject"></param>
    public static void RemoveObject(string path, GameObject gameObject)
    {
        if( itemList == null || !itemList.ContainsKey( path ) ) return;
        //移除路径下该对象
        itemList[path].RemoveObject( gameObject );
    }

    /// <summary>
    /// 获取缓存对象
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject GetObject(string path)
    {
        if(itemList ==null||!itemList.ContainsKey(path)) return null;
        //可能激活多个对象
        return itemList[path].GetObject();
    }

    /// <summary>
    /// 禁用对象池对象
    /// </summary>
    /// <param name="path"></param>
    /// <param name="gameObject"></param>
    public static void DestoryObject(string path,GameObject gameObject)
    {
        if( itemList == null || !itemList.ContainsKey( path ) ) return;
        itemList[path].DestoryItem( gameObject );
    }

    /// <summary>
    /// 处理超时对象，禁用
    /// </summary>
    public static void ExpireObject()
    {
        //筛选
        foreach( ItemPool item in itemList.Values )
        {
            item.ExpireObject();
        }
    }

    /// <summary>
    /// 销毁对象池
    /// </summary>
    public static void Destroy()
    {
        foreach( ItemPool item in itemList.Values )
        {
            item.Destory();
        }
        itemList = null;
    }
}
