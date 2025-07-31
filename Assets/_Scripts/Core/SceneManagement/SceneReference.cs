using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SceneReference
{
    [SerializeField]
    private Object m_SceneAsset;

    [SerializeField]
    private string m_SceneName = "";
    public string SceneName
    {
        get { return m_SceneName; }
    }
    
    public static implicit operator string( SceneReference sceneField )
    {
        return sceneField.SceneName;
    }
    
    public static bool operator == (SceneReference a, SceneReference b)
    {
        if (a == null || b == null) return false;
        return a.SceneName == b.SceneName;
    }

    public static bool operator !=(SceneReference a, SceneReference b)
    {
        return !(a == b);
    }

    public override bool Equals(object sceneReference)
    {
        SceneReference scene = (SceneReference)sceneReference;
        if (scene == null) return false;

        return SceneName.Equals(scene.SceneName);
    }

    public override int GetHashCode()
    {
        return SceneName.GetHashCode();
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneFieldPropertyDrawer : PropertyDrawer 
{
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        EditorGUI.BeginProperty(_position, GUIContent.none, _property);
        SerializedProperty sceneAsset = _property.FindPropertyRelative("m_SceneAsset");
        SerializedProperty sceneName = _property.FindPropertyRelative("m_SceneName");
        _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
        if (sceneAsset != null)
        {
            sceneAsset.objectReferenceValue = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false); 

            if( sceneAsset.objectReferenceValue != null )
            {
                sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
            }
        }
        EditorGUI.EndProperty( );
    }
}
#endif