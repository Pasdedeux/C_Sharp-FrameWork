using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 对象池数据
/// </summary>
public class ItemPool 
{
    /// <summary>
    /// 对象路径
    /// </summary>
    public string path;

    /// <summary>
    /// 对象列表
    /// </summary>
    public Dictionary<int, ItemPoolTime> itemTimeList;

    public ItemPool(string path)
    {
        this.path = path;
        this.itemTimeList = new Dictionary<int, ItemPoolTime>();
    }

    /// <summary>
    /// 添加对象。若对象池中已存在该对象，则直接激活
    /// </summary>
    /// <param name="gameObj"></param>
    public void PushItem(GameObject gameObj)
    {
        int hasKey = gameObj.GetHashCode();
        if(!this.itemTimeList.ContainsKey(hasKey))
        {
            this.itemTimeList.Add( hasKey, new ItemPoolTime( gameObj ) );
        }
        else
        {
            this.itemTimeList[hasKey].Active();
        }
    }

    /// <summary>
    /// 对象执行销毁（去激活）
    /// </summary>
    /// <param name="gameObj"></param>
    public void DestoryItem(GameObject gameObj)
    {
        int key = gameObj.GetHashCode();
        if(this.itemTimeList.ContainsKey(key))
        {
            this.itemTimeList[key].Destory();
        }
    }

    /// <summary>
    /// 获取对象池中对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        if( this.itemTimeList == null || this.itemTimeList.Count == 0 ) return null;

        foreach(ItemPoolTime itemPoolTime in this.itemTimeList.Values)
        {
            if( itemPoolTime.destoryStates )
                return itemPoolTime.Active();
        }

        return null;
    }

    /// <summary>
    /// 移除并且销毁池中某个对象
    /// </summary>
    /// <param name="gameObj"></param>
    public void RemoveObject(GameObject gameObj)
    {
        int hashKey = gameObj.GetHashCode();
        if(this.itemTimeList.ContainsKey(hashKey))
        {
            //移除舞台上对象
            GameObject.Destroy( gameObj );
            //从字典中移除
            this.itemTimeList.Remove( hashKey );
        }
    }

    /// <summary>
    /// 池销毁对象
    /// </summary>
    public void Destory()
    {
        IList<ItemPoolTime> poolList = new List<ItemPoolTime>();

        foreach( ItemPoolTime itempool in this.itemTimeList.Values )
        {
            poolList.Add( itempool );
        }
        while( poolList.Count > 0 )
        {
            if( poolList[0] != null && poolList[0].gameObj != null )
            {
                GameObject.Destroy( poolList[0].gameObj );
                poolList.RemoveAt( 0 );
            }
        }
        this.itemTimeList = new Dictionary<int, ItemPoolTime>();
    }

    /// <summary>
    /// 超时检测
    /// </summary>
    public void ExpireObject()
    {
        IList<ItemPoolTime> expireList = new List<ItemPoolTime>();
        foreach( ItemPoolTime item in this.itemTimeList.Values )
        {
            if( item.IsExpire() ) expireList.Add( item );
        }
        int expireCount = expireList.Count;
        for( int index = 0; index < expireCount; index++ )
        {
            this.RemoveObject( expireList[index].gameObj );
        }
    }
}
