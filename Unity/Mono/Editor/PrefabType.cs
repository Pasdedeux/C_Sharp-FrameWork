using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 预制件类型
/// </summary>
public class PrefabType : MonoBehaviour 
{
    public enum InType
    {
        None,
        /// <summary>
        /// 地形
        /// </summary>
        Landform,
        /// <summary>
        /// 障碍物
        /// </summary>
        Obstacle,
        /// <summary>
        /// 起点
        /// </summary>
        Player,
        /// <summary>
        /// 终点
        /// </summary>
        Hole,
        /// <summary>
        /// 道具
        /// </summary>
        Prop,
    }
    public InType type;

    public enum PropType
    {
        /// <summary>
        /// 金币
        /// </summary>
        Gold,
        /// <summary>
        /// 道具1
        /// </summary>
        Item1,
        /// <summary>
        /// 道具2
        /// </summary>
        Item2,
    }
    [HideInInspector]
    public PropType propType;

    [HideInInspector]
    [Range( 1 , 5 )]
    public int goldLevel;

}
