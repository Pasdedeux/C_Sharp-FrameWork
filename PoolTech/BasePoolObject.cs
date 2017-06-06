using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可被放入对象池的物体
/// </summary>
public class BasePoolObject : MonoBehaviour
{
    /// <summary>
    /// 该对象归属的池类型
    /// </summary>
    public PoolType poolType;

    private float _lifeTime = 0f, _originalTime = 0f;
    /// <summary>
    /// 自我销毁（放入池中）的时间
    /// </summary>
    protected float lifeTime
    {
        get { return _lifeTime; }
        set
        {
            _lifeTime = value;
            _originalTime = value;
        }
    }


    /// <summary>
    /// 初始化该对象状态为取出时的初始状态
    /// 示例只做出模型尺寸的调整
    /// </summary>
    public virtual void InitStatus( )
    {
        //将剩余生命时间赋值为设定生命时间
        _lifeTime = _originalTime;
        //模型尺寸初始化
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler( Vector3.zero );
    }

    /// <summary>
    /// 对于需要定时销毁的物体，在父类update中持续调用
    /// </summary>
	public void PoolUpdate( )
    {
        if ( _lifeTime > 0 )
        {
            _lifeTime -= Time.deltaTime;
            //生命持续时间结束后回收入池
            if ( _lifeTime <= 0 ) DoDestroy();
        }
    }

    /// <summary>
    /// 将对象初始化，回收入池
    /// </summary>
    public void DoDestroy( )
    {
        PoolsManager.Instance.Recycle( this.gameObject );
    }
}
