//*****************************
//  BaseUnitySingleton
//
//  Created by Derek Liu
//
//*****************************


namespace BaseClassKit
{
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// 组件单例基类
    /// </summary>
    /// <typeparam name="T">继承子类类名</typeparam>
    /// 
    public class BaseUnitySingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                //是否已存在单例挂载对象
                if( _instance == null )
                {
                    _instance = FindObjectOfType( typeof( T ) ) as T;

                    //若不存在则创建一个【隐匿对象】将（继承类）以组件方式挂载
                    if( _instance == null )
                    {
                        GameObject gObj = new GameObject();
                        gObj.name = "@Singleton(" + typeof( T ).Name + ")";
                        gObj.hideFlags = HideFlags.HideAndDontSave;//可见性，以及不可消除性

                        //这里会先触发继承类Awake()方法
                        _instance = gObj.AddComponent( typeof( T ) ) as T;
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        public virtual void onDestroy()
        {
            if( _instance != null )
            {
                Destroy( _instance );
                _instance = null;
            }
        }

        /// <summary>
        /// 应用程序退出
        /// </summary>
        public virtual void OnApplicationQuit()
        {
            if( _instance != null )
            {
                Destroy( _instance );
                _instance = null;
            }
        }
    }
}
