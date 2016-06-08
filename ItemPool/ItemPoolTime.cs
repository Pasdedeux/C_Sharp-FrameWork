using UnityEngine;
using System.Collections;

public class ItemPoolTime
{
    /// <summary>
    /// 存储对象
    /// </summary>
    public GameObject gameObj;

    /// <summary>
    /// 有效实例化时间
    /// </summary>
    public float expireTime;

    /// <summary>
    /// 销毁状态
    /// </summary>
    public bool destoryStates{get;private set;}

    public ItemPoolTime(GameObject gObj)
    {
        this.gameObj = gObj;
        this.destoryStates = false;
    }

    /// <summary>
    /// 激活对象
    /// </summary>
    /// <returns></returns>
    public GameObject Active()
    {
        this.gameObj.SetActive( true );
        this.destoryStates = false;
        return this.gameObj;
    }

    /// <summary>
    /// 销毁对象
    /// </summary>
    public void Destory()
    {
        this.gameObj.SetActive( false );
        this.destoryStates = true;
        this.expireTime = Time.time;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value><c>true</c> if this instance is expire; otherwise, <c>false</c>.</value>
    public bool IsExpire()
    {
        if( !this.destoryStates ) return false;
        if( Time.time - this.expireTime >= ItemPoolManager.EXPIRE_TIME ) return true;

        return false;
    }
}
