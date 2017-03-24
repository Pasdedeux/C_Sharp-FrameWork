using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 该文件只需放置在Editor文件夹下，不一定是Asset/Editor/
/// </summary>
[CustomEditor( typeof( PrefabType ) )]
[CanEditMultipleObjects]
public class PrefabTypeEditor : Editor
{
    private PrefabType _pt;
    private SerializedObject _ptObj;
    private SerializedProperty _type, _propType;
    private SerializedProperty _goldLevel;


    private void OnEnable( )
    {
        _ptObj = new SerializedObject( target );
        _type = _ptObj.FindProperty( "type" );
        _propType = _ptObj.FindProperty( "propType" );
        _goldLevel = _ptObj.FindProperty( "goldLevel" );
    }

    public override void OnInspectorGUI( )
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        _pt = ( PrefabType ) target;

        //道具类型包含子菜单
        if ( _pt.type == PrefabType.InType.Prop )
        {
            _pt.propType = ( PrefabType.PropType ) EditorGUILayout.EnumPopup( "PropType:" , _pt.propType );

            //道具类型为金币时显示金币数量
            if ( _pt.propType == PrefabType.PropType.Gold )
            {
                EditorGUILayout.PropertyField( _goldLevel );
            }

        }

        serializedObject.ApplyModifiedProperties();
    }
}
