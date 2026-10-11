using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Timer))]
public class TimerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        SerializedProperty maxTimeProperty = property.FindPropertyRelative("<MaxTime>k__BackingField");
        SerializedProperty canOvercountProperty = property.FindPropertyRelative("<CanOvercount>k__BackingField");

        Rect rect;
        EditorGUI.BeginChangeCheck();
        {
            float newMaxTime = EditorGUI.FloatField(position, "Max Time", maxTimeProperty.floatValue);
            rect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.objectField)
                .With(x: position.x, w: position.width);
            bool newCanOvercount = EditorGUI.Toggle(rect, "Can Overcount", canOvercountProperty.boolValue);
            if (EditorApplication.isPlaying)
            {
                EditorGUI.BeginDisabledGroup(true);
                {
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                    Timer t = fieldInfo.GetValue(property.serializedObject.targetObject) as Timer;
                    rect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.objectField)
                        .With(x: position.x, w: position.width);
                    EditorGUI.FloatField(rect, "Current Time", t.CurrentTime);
                }
                EditorGUI.EndDisabledGroup();
            }
            if (EditorGUI.EndChangeCheck())
            {
                maxTimeProperty.floatValue = newMaxTime;
                canOvercountProperty.boolValue = newCanOvercount;
            }
        }

        EditorGUI.EndProperty();
    }
}
